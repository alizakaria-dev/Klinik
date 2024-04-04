import { configureStore } from "@reduxjs/toolkit";
import DoctorReducer from "./DoctorReducer";
import FeatureReducer from "./FeatureReducer";
import HealthCareReducer from "./HealthCareReducer";
import TestimonialReducer from "./TestimonialReducer";
import AppointmentReducer from "./AppointmentReducer";
import InfoReducer from "./InfoReducer";
import UserReducer from "./UserReducer";
import FileReducer from "./FileReducer";

export const store = configureStore({
  reducer: {
    doctor: DoctorReducer,
    feature: FeatureReducer,
    service: HealthCareReducer,
    testimonial: TestimonialReducer,
    appointment: AppointmentReducer,
    info: InfoReducer,
    user: UserReducer,
    file: FileReducer,
  },
});

export type RootState = ReturnType<typeof store.getState>;
export type AppDispatch = typeof store.dispatch;
