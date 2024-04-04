import { createAsyncThunk, createSlice } from "@reduxjs/toolkit";
import { Result, baseUrl } from "../Shared/Result";
import { jwtDecode } from "jwt-decode";

export class User {
  USERID: number;
  ROLEID: number;
  USERNAME: string;
  PASSWORD: string;
  ROLENAME: string;
  Token: string;
}

export class Login {
  USERNAME: string;
  PASSWORD: string;
}

interface DataState {
  data: GetLoginResult;
  loading: "idle" | "pending" | "succeeded" | "failed";
  error: string | null;
  token: string;
  role: number;
  isAuthenticated: boolean;
}

const initialState: DataState = {
  data: null,
  loading: "idle",
  error: null,
  token: "",
  role: 0,
  isAuthenticated: false,
};

export class GetLoginResult extends Result {
  override MyResult: User = {
    USERID: 0,
    ROLEID: 0,
    USERNAME: "",
    PASSWORD: "",
    ROLENAME: "",
    Token: "",
  };
}

export const addTokenToLocalStorage = (token: string) => {
  localStorage.setItem("jwt-token", token);
};

export const loginUser = createAsyncThunk<GetLoginResult, Login>(
  "user/login",
  async (login: Login) => {
    try {
      const result = await fetch(baseUrl + "Users/Login", {
        method: "POST",
        headers: {
          "Content-Type": "application/json",
        },
        body: JSON.stringify(login),
      });

      const body: GetLoginResult = await result.json();

      console.log(body);

      if (body.IsSuccess === true && body.MyResult) {
        addTokenToLocalStorage(body.MyResult.Token);
      }

      return body;
    } catch (error) {
      console.error(error);
    }
  }
);

export function GetTokenFromLocalStorage(): string {
  let token: string = "";
  if (typeof localStorage !== "undefined") {
    token = localStorage.getItem("jwt-token");
  }

  return token;
}

const userSlice = createSlice({
  name: "user",
  initialState,
  reducers: {
    setToken(state, action) {
      state.token = action.payload;
    },
    clearToken(state) {
      localStorage.removeItem("jwt-token");
      state.token = "";
      state.role = 0;
      state.isAuthenticated = false;
    },
    getUserRole(state, action) {
      var role: any = jwtDecode(action.payload);
      var roleId = role.Role;
      state.role = roleId;
    },
    checkLocalStorageValue(state) {
      const token = GetTokenFromLocalStorage();
      if (token) {
        state.isAuthenticated = true;
      }
    },
  },
  extraReducers: (builder) => {
    builder
      .addCase(loginUser.pending, (state) => {
        state.loading = "pending";
      })
      .addCase(loginUser.fulfilled, (state, action) => {
        state.loading = "succeeded";
        state.data = action.payload;
        state.role = action.payload.MyResult.ROLEID;
        state.isAuthenticated = true;
      })
      .addCase(loginUser.rejected, (state, action) => {
        state.loading = "failed";
        state.error = action.error.message ?? "An error occurred";
      });
  },
});

export const { setToken, clearToken, getUserRole, checkLocalStorageValue } =
  userSlice.actions;
export default userSlice.reducer;
