import About from "../components/About/About";
import AppointmentComponent from "../components/Appointment/Appointment";
import FeatureComponent from "../components/Feature/Feature";
import HealthCare from "../components/HealthCare/HealthCare";
import Info from "../components/Info/Info";
import "@fortawesome/fontawesome-free/css/all.min.css";
import Testimonial from "../components/Testimonial/Testimonial";
import Footer from "../components/Footer/Footer";
import { useDispatch, useSelector } from "react-redux";
import { useEffect } from "react";
import {
  DeleteDoctorResult,
  Doctor,
  deleteDoctor,
  getDoctors,
} from "../redux/DoctorReducer";
import { AppDispatch, RootState } from "../redux/store";
import DoctorComponent from "../components/Doctor/Doctor";
import { getFeatures } from "../redux/FeatureReducer";
import { getServices } from "../redux/HealthCareReducer";
import { getTestimonials } from "../redux/TestimonialReducer";
import {
  Appointment,
  CreateAppointmentResult,
  createAppointment,
} from "../redux/AppointmentReducer";
import { getInfo } from "../redux/InfoReducer";
import {
  GetTokenFromLocalStorage,
  checkLocalStorageValue,
  getUserRole,
  setToken,
} from "../redux/UserReducer";

export default function Home() {
  const dispatch = useDispatch<AppDispatch>();

  const doctorsState = useSelector((state: RootState) => state.doctor);
  const featureState = useSelector((state: RootState) => state.feature);
  const serviceState = useSelector((state: RootState) => state.service);
  const testimonialState = useSelector((state: RootState) => state.testimonial);
  const infoState = useSelector((state: RootState) => state.info);
  const userState = useSelector((state: RootState) => state.user);

  const handleFormSubmit = async (formData: Appointment) => {
    const result = await dispatch(createAppointment(formData));
    console.log(
      "result of adding appointment",
      result.payload as CreateAppointmentResult
    );
  };

  const handleItemClick = async (itemId: number) => {
    const result = await dispatch(
      deleteDoctor({ id: itemId, token: userState.token })
    );

    const resultMessage = result.payload as DeleteDoctorResult;

    console.log(
      "result of deleting the doctor",
      result.payload as DeleteDoctorResult
    );

    if (resultMessage.IsSuccess == true) {
      dispatch(getDoctors());
    }
  };

  useEffect(() => {
    const fetchData = async () => {
      try {
        // const [doctors, features, services, testimonials, info] =
        await Promise.all([
          dispatch(getDoctors()),
          dispatch(getFeatures()),
          dispatch(getServices()),
          dispatch(getTestimonials()),
          dispatch(getInfo()),
        ]);

        const localStorageValue = GetTokenFromLocalStorage();

        if (localStorageValue) {
          dispatch(setToken(localStorageValue));

          dispatch(getUserRole(localStorageValue));
        }

        dispatch(checkLocalStorageValue());
      } catch (error) {
        console.error("Error fetching data:", error);
      }
    };

    fetchData();
  }, []);

  return (
    <>
      {infoState.data ? <Info info={infoState.data.MyResult} /> : null}

      <About />
      {serviceState.data ? (
        <HealthCare services={serviceState.data.MyResult} />
      ) : (
        []
      )}
      {featureState.data ? (
        <FeatureComponent feature={featureState.data.MyResult} />
      ) : (
        []
      )}

      {doctorsState.data ? (
        <DoctorComponent
          doctors={doctorsState.data.MyResult as Doctor[]}
          role={userState.role.toString()}
          handleDoctorId={handleItemClick}
        />
      ) : (
        []
      )}
      <AppointmentComponent onFormSubmit={handleFormSubmit} />

      {testimonialState.data ? (
        <Testimonial testimonials={testimonialState.data.MyResult} />
      ) : (
        []
      )}

      <Footer />
    </>
  );
}
