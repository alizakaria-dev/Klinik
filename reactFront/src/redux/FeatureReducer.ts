import { createAsyncThunk, createSlice } from "@reduxjs/toolkit";
import { Result, baseUrl } from "../Shared/Result";

class GetFeaturesResult extends Result {
  override MyResult: Feature = {
    FEATUREID: 0,
    TEXT: "",
    TITLE: "",
    FEATUREITEMS: [],
  };
}

interface DataState {
  data: GetFeaturesResult; // Define your data type
  loading: "idle" | "pending" | "succeeded" | "failed";
  error: string | null;
}

const initialState: DataState = {
  data: null,
  loading: "idle",
  error: null,
};

export const getFeatures = createAsyncThunk<GetFeaturesResult>(
  "features/GetFeatures",
  async () => {
    try {
      const result = await fetch(baseUrl + "Features/GetFeature");

      const body: GetFeaturesResult = await result.json();

      return body;
    } catch (error) {
      console.error(error);
    }
  }
);

const featureSlice = createSlice({
  name: "features",
  initialState,
  reducers: {},
  extraReducers: (builder) => {
    builder
      .addCase(getFeatures.pending, (state) => {
        state.loading = "pending";
      })
      .addCase(getFeatures.fulfilled, (state, action) => {
        state.loading = "succeeded";
        state.data = action.payload;
      })
      .addCase(getFeatures.rejected, (state, action) => {
        state.loading = "failed";
        state.error = action.error.message ?? "An error occurred";
      });
  },
});

export class FeatureItem {
  FEATUREITEMID: number;
  FEATUREID: number;
  ICON: string;
  TEXTONE: string;
  TEXTTWO: string;
}

export class Feature {
  FEATUREID: number;
  TEXT: string;
  TITLE: string;
  FEATUREITEMS: FeatureItem[];
}

export default featureSlice.reducer;
