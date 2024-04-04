import { createAsyncThunk, createSlice } from "@reduxjs/toolkit";
import { Result, baseUrl } from "../Shared/Result";

export class Files {
  FILEID: number;
  REL_KEY: number;
  long: number;
  REL_TABLE: string;
  REL_FIELD: string;
  EXTENSION: string;
  URL: string;
}

export class UploadFileResult extends Result {
  override MyResult: Files = {
    FILEID: 0,
    REL_KEY: 0,
    long: 0,
    REL_TABLE: "",
    REL_FIELD: "",
    EXTENSION: "",
    URL: "",
  };
}

export class DeleteFileResult extends Result {}

interface DataState {
  data: UploadFileResult | null;
  loading: "idle" | "pending" | "succeeded" | "failed";
  error: string | null;
}

const initialState: DataState = {
  data: null,
  loading: "idle",
  error: null,
};

export const uploadFile = createAsyncThunk<
  UploadFileResult,
  { file: File; relKey: number; relTable: string; relField: string }
>("file/uploadFile", async ({ file, relKey, relTable, relField }) => {
  try {
    const formData = new FormData();
    formData.append("file", file);

    const result = await fetch(
      `${baseUrl}Files/UploadFile?relKey=${relKey}&relTable=${relTable}&relField=${relField}`,
      {
        method: "POST",
        body: formData,
      }
    );

    const body: UploadFileResult = await result.json();

    return body;
  } catch (error) {
    console.error(error);
  }
});

export const deleteFile = createAsyncThunk<
  DeleteFileResult,
  { id: number; token: string }
>("file/DeleteFile", async ({ id, token }) => {
  try {
    const result = await fetch(`${baseUrl}Files/DeleteFile?id=${id}`, {
      method: "DELETE",
      headers: {
        Authorization: `${token}`,
      },
    });

    const body: DeleteFileResult = await result.json();

    return body;
  } catch (error) {
    console.error(error);
  }
});

const fileSlice = createSlice({
  name: "file",
  initialState,
  reducers: {},
  extraReducers: (builder) => {
    builder
      .addCase(uploadFile.pending, (state) => {
        state.loading = "pending";
      })
      .addCase(uploadFile.fulfilled, (state, action) => {
        state.loading = "succeeded";
        state.data = action.payload;
      })
      .addCase(uploadFile.rejected, (state, action) => {
        state.loading = "failed";
        state.error = action.error.message ?? "An error occurred";
      });
  },
});

export default fileSlice.reducer;
