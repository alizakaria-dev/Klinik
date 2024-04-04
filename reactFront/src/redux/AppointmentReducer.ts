import { createAsyncThunk, createSlice } from "@reduxjs/toolkit";
import { Result, baseUrl } from "../Shared/Result";

export class Appointment {
  APPOINTMENTID: number;
  NAME: string;
  EMAIL: string;
  DOCTOR: string;
  MOBILE: string;
  DESCRIPTION: string;
  DATE: Date;
  TIME: string;
}

export class GetAppointmentsResult extends Result {
  override MyResult: Appointment[] = [];
}

export class CreateAppointmentResult extends Result {
  override MyResult: Appointment = {
    APPOINTMENTID: 0,
    NAME: "",
    EMAIL: "",
    DOCTOR: "",
    MOBILE: "",
    DESCRIPTION: "",
    DATE: undefined,
    TIME: "",
  };
}

interface DataState {
  data: CreateAppointmentResult | GetAppointmentsResult;
  loading: "idle" | "pending" | "succeeded" | "failed";
  error: string | null;
}

const initialState: DataState = {
  data: null,
  loading: "idle",
  error: null,
};

export const createAppointment = createAsyncThunk<
  CreateAppointmentResult,
  Appointment
>("appointment/createAppointment", async (appointment: Appointment) => {
  try {
    const result = await fetch(baseUrl + "Appointments/CreateAppointment", {
      method: "POST",
      headers: {
        "Content-Type": "application/json",
      },
      body: JSON.stringify(appointment),
    });

    const body: CreateAppointmentResult = await result.json();

    return body;
  } catch (error) {
    console.error(error);
  }
});

export const getAppointments = createAsyncThunk<GetAppointmentsResult>(
  "appointment/getAppointments",
  async () => {
    try {
      const result = await fetch(baseUrl + "Appointments/GetAllAppointments", {
        method: "GET",
        headers: {
          "Content-Type": "application/json",
        },
      });

      const body: GetAppointmentsResult = await result.json();

      return body;
    } catch (error) {
      console.error(error);
    }
  }
);

const appointmentSlice = createSlice({
  name: "appointment",
  initialState,
  reducers: {},
  extraReducers: (builder) => {
    builder
      .addCase(createAppointment.fulfilled, (state, action) => {
        state.loading = "succeeded";
        state.data = action.payload;
      })
      .addCase(getAppointments.fulfilled, (state, action) => {
        state.loading = "succeeded";
        state.data = action.payload;
      });
  },
});

export default appointmentSlice.reducer;
