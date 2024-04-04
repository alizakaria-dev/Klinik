import { useDispatch } from "react-redux";
import { AppDispatch } from "../redux/store";
import { useEffect, useState } from "react";
import {
  GetAppointmentsResult,
  getAppointments,
} from "../redux/AppointmentReducer";
import styles from "./Appointments.module.css";

export default function Appointments() {
  const dispatch = useDispatch<AppDispatch>();

  const [appointments, setAppointments] = useState<GetAppointmentsResult>(null);

  useEffect(() => {
    const abortController = new AbortController();

    const fetchData = async () => {
      try {
        var result = await dispatch(getAppointments());

        var resultBody = result.payload as GetAppointmentsResult;

        setAppointments(resultBody);
      } catch (error) {
        console.error(error);
      }
    };

    fetchData();

    return () => {
      abortController.abort();
    };
  }, []);

  return (
    <table className={styles.table}>
      <thead>
        <tr>
          <th className={styles.th}>Date</th>
          <th className={styles.th}>Time</th>
          <th className={styles.th}>Name</th>
          <th className={styles.th}>Mobile</th>
          <th className={styles.th}>Email</th>
          <th className={styles.th}>Doctor</th>
          <th className={styles.th}>Description</th>
        </tr>
      </thead>
      <tbody>
        {appointments
          ? appointments.MyResult?.map((appointment) => (
              <tr key={appointment.APPOINTMENTID}>
                <td className={styles.td}>{appointment.DATE.toString()}</td>
                <td className={styles.td}>{appointment.TIME}</td>
                <td className={styles.td}>{appointment.NAME}</td>
                <td className={styles.td}>{appointment.MOBILE}</td>
                <td className={styles.td}>{appointment.EMAIL}</td>
                <td className={styles.td}>{appointment.DOCTOR}</td>
                <td className={styles.td}>{appointment.DESCRIPTION}</td>
              </tr>
            ))
          : null}
      </tbody>
    </table>
  );
}
