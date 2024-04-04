import { createAsyncThunk, createSlice } from "@reduxjs/toolkit";
import { Result, baseUrl } from "../Shared/Result";

export class Information {
  INFOID: number;
  DOCTORS: number;
  PATIENTS: number;
  STAFF: number;
  Files: File[];
}

export class GetInfoResult extends Result {
  override MyResult: Information = {
    INFOID: 0,
    DOCTORS: 0,
    PATIENTS: 0,
    STAFF: 0,
    Files: [],
  };
}

interface DataState {
  data: GetInfoResult;
  loading: "idle" | "pending" | "succeeded" | "failed";
  error: string | null;
}

const initialState: DataState = {
  data: null,
  loading: "idle",
  error: null,
};

export const getInfo = createAsyncThunk<GetInfoResult>(
  "info/getInfo",
  async () => {
    try {
      const result = await fetch(baseUrl + "Info/GetInfo");

      const body: GetInfoResult = await result.json();

      return body;
    } catch (error) {
      console.error(error);
    }
  }
);

export const infoSlice = createSlice({
  name: "info",
  initialState,
  reducers: {},
  extraReducers: (builder) => {
    builder.addCase(getInfo.fulfilled, (state, action) => {
      state.loading = "succeeded";
      state.data = action.payload;
    });
  },
});

export default infoSlice.reducer;
