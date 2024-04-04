import { createAsyncThunk, createSlice } from "@reduxjs/toolkit";
import { Result, baseUrl } from "../Shared/Result";

export class Service {
  SERVICEID: number;
  ICON: string;
  TITLE: string;
  TEXT: string;
}

export class GetAllServicesResult extends Result {
  override MyResult: Service[] = [];
}

interface DataState {
  data: GetAllServicesResult;
  loading: "idle" | "pending" | "succeeded" | "failed";
  error: string | null;
}

const initialState: DataState = {
  data: null,
  loading: "idle",
  error: null,
};

export const getServices = createAsyncThunk<GetAllServicesResult>(
  "service/GetServices",
  async () => {
    try {
      const result = await fetch(baseUrl + "Services/GetAllServices");

      const body: GetAllServicesResult = await result.json();

      return body;
    } catch (error) {
      console.error(error);
    }
  }
);

const serviceSlice = createSlice({
  name: "doctors",
  initialState,
  reducers: {},
  extraReducers: (builder) => {
    builder
      .addCase(getServices.pending, (state) => {
        state.loading = "pending";
      })
      .addCase(getServices.fulfilled, (state, action) => {
        state.loading = "succeeded";
        state.data = action.payload;
      })
      .addCase(getServices.rejected, (state, action) => {
        state.loading = "failed";
        state.error = action.error.message ?? "An error occurred";
      });
  },
});

export default serviceSlice.reducer;
