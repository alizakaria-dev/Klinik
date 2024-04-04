import { createAsyncThunk, createSlice } from "@reduxjs/toolkit";
import { Result, baseUrl } from "../Shared/Result";
import { Files } from "./FileReducer";

export class GetDoctorsResult extends Result {
  override MyResult: Doctor[] = [];
}
export class GetDoctorResult extends Result {
  override MyResult: Doctor = {
    DOCTORID: 0,
    NAME: "",
    DEPARTMENT: "",
    FACEBOOKLINK: "",
    TWITTERLINK: "",
    INSTAGRAMLINK: "",
    DESCRIPTION: "",
    Files: [],
  };
}
export class DeleteDoctorResult extends Result {}

export class CreateDoctorResult extends Result {
  override MyResult: Doctor = {
    DOCTORID: 0,
    NAME: "",
    DEPARTMENT: "",
    FACEBOOKLINK: "",
    TWITTERLINK: "",
    INSTAGRAMLINK: "",
    DESCRIPTION: "",
    Files: [],
  };
}
export class UpdateDoctorResult extends Result {
  override MyResult: Doctor = {
    DOCTORID: 0,
    NAME: "",
    DEPARTMENT: "",
    FACEBOOKLINK: "",
    TWITTERLINK: "",
    INSTAGRAMLINK: "",
    DESCRIPTION: "",
    Files: [],
  };
}

interface DataState {
  data:
    | GetDoctorsResult
    | DeleteDoctorResult
    | GetDoctorResult
    | UpdateDoctorResult
    | null;
  loading: "idle" | "pending" | "succeeded" | "failed";
  error: string | null;
  isAuthenticated: boolean;
}

const initialState: DataState = {
  data: null,
  loading: "idle",
  error: null,
  isAuthenticated: false,
};

export const getDoctors = createAsyncThunk<GetDoctorsResult>(
  "doctor/GetDoctors",
  async () => {
    try {
      const result = await fetch(baseUrl + "Doctors/GetAllDoctors");

      const body: GetDoctorsResult = await result.json();

      return body;
    } catch (error) {
      console.error(error);
    }
  }
);

export const getDoctor = createAsyncThunk<GetDoctorResult, number>(
  "doctor/getDoctor",
  async (id: number) => {
    try {
      const result = await fetch(baseUrl + "Doctors/GetDoctorById?id=" + id, {
        method: "GET",
        headers: {
          "Content-type": "application/json",
        },
      });

      const body: GetDoctorResult = await result.json();

      return body;
    } catch (error) {
      console.error(error);
    }
  }
);

export const deleteDoctor = createAsyncThunk<
  DeleteDoctorResult,
  { id: number; token: string }
>("doctor/deleteDoctor", async ({ id, token }) => {
  try {
    const result = await fetch(baseUrl + "Doctors/DeleteDoctor?id=" + id, {
      method: "DELETE",
      headers: {
        "Content-type": "application/json",
        Authorization: `${token}`,
      },
    });

    const body: DeleteDoctorResult = await result.json();

    return body;
  } catch (error) {
    console.error(error);
  }
});

export const createDoctor = createAsyncThunk<
  CreateDoctorResult,
  { doctor: Doctor; token: string }
>("doctor/createDoctor", async ({ doctor, token }) => {
  try {
    const result = await fetch(baseUrl + "Doctors/CreateDoctor", {
      method: "POST",
      headers: {
        "Content-Type": "application/json",
        Authorization: `${token}`,
      },
      body: JSON.stringify(doctor),
    });

    const body: CreateDoctorResult = await result.json();

    return body;
  } catch (error) {
    console.error(error);
  }
});

export const updateDoctor = createAsyncThunk<
  UpdateDoctorResult,
  { doctor: Doctor; token: string }
>("doctor/updateDoctor", async ({ doctor, token }) => {
  try {
    const result = await fetch(baseUrl + "Doctors/UpdateDoctor", {
      method: "PUT",
      headers: {
        "Content-Type": "application/json",
        Authorization: `${token}`,
      },
      body: JSON.stringify(doctor),
    });

    const body: UpdateDoctorResult = await result.json();

    return body;
  } catch (error) {
    console.error(error);
  }
});

const doctorsSlice = createSlice({
  name: "doctors",
  initialState,
  reducers: {},
  extraReducers: (builder) => {
    builder
      .addCase(getDoctors.pending, (state) => {
        state.loading = "pending";
      })
      .addCase(getDoctors.fulfilled, (state, action) => {
        state.loading = "succeeded";
        state.data = action.payload;
      })
      .addCase(getDoctors.rejected, (state, action) => {
        state.loading = "failed";
        state.error = action.error.message ?? "An error occurred";
      })
      .addCase(deleteDoctor.pending, (state) => {
        state.loading = "pending";
      })
      .addCase(deleteDoctor.fulfilled, (state, action) => {
        state.loading = "succeeded";
        state.data = action.payload;
      })
      .addCase(deleteDoctor.rejected, (state, action) => {
        state.loading = "failed";
        state.error = action.error.message ?? "An error occurred";
      })
      .addCase(createDoctor.pending, (state) => {
        state.loading = "pending";
      })
      .addCase(createDoctor.fulfilled, (state, action) => {
        state.loading = "succeeded";
        state.data = action.payload;
      })
      .addCase(createDoctor.rejected, (state, action) => {
        state.loading = "failed";
        state.error = action.error.message ?? "An error occurred";
      })
      .addCase(getDoctor.pending, (state) => {
        state.loading = "pending";
      })
      .addCase(getDoctor.fulfilled, (state, action) => {
        state.loading = "succeeded";
        state.data = action.payload;
      })
      .addCase(getDoctor.rejected, (state, action) => {
        state.loading = "failed";
        state.error = action.error.message ?? "An error occurred";
      })
      .addCase(updateDoctor.pending, (state) => {
        state.loading = "pending";
      })
      .addCase(updateDoctor.fulfilled, (state, action) => {
        state.loading = "succeeded";
        state.data = action.payload;
      })
      .addCase(updateDoctor.rejected, (state, action) => {
        state.loading = "failed";
        state.error = action.error.message ?? "An error occurred";
      });
  },
});

export class Doctor {
  DOCTORID: number;
  NAME: string;
  DEPARTMENT: string;
  FACEBOOKLINK: string;
  TWITTERLINK: string;
  INSTAGRAMLINK: string;
  DESCRIPTION: string;
  Files: Files[];
}

export const selectData = (state: { data: DataState }) => state.data;

export default doctorsSlice.reducer;
