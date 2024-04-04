import { createAsyncThunk, createSlice } from "@reduxjs/toolkit";
import { Result, baseUrl } from "../Shared/Result";
import { Files } from "./FileReducer";

export class GetTestimonialsResult extends Result {
  override MyResult: Testimonial[] = [];
}

export class Testimonial {
  TESTIMONIALID: number;
  USERNAME: string;
  PROFESSION: string;
  DESCRIPTION: string;
  File: Files;
}

interface DataState {
  data: GetTestimonialsResult;
  loading: "idle" | "pending" | "succeeded" | "failed";
  error: string | null;
}

const initialState: DataState = {
  data: null,
  loading: "idle",
  error: null,
};

export const getTestimonials = createAsyncThunk<GetTestimonialsResult>(
  "testimonial/GetTestimonials",
  async () => {
    try {
      const result = await fetch(baseUrl + "Testimonials/GetAllTestimonials");

      const body: GetTestimonialsResult = await result.json();

      return body;
    } catch (error) {
      console.error(error);
    }
  }
);

const doctorsSlice = createSlice({
  name: "testimonial",
  initialState,
  reducers: {},
  extraReducers: (builder) => {
    builder
      .addCase(getTestimonials.pending, (state) => {
        state.loading = "pending";
      })
      .addCase(getTestimonials.fulfilled, (state, action) => {
        state.loading = "succeeded";
        state.data = action.payload;
      })
      .addCase(getTestimonials.rejected, (state, action) => {
        state.loading = "failed";
        state.error = action.error.message ?? "An error occurred";
        console.error(state.error);
      });
  },
});

export default doctorsSlice.reducer;
