import { useState } from "react";
import Input from "../Input/Input";
import styles from "./Appointment.module.css";
import { Appointment } from "../../redux/AppointmentReducer";

interface AppointmentFormValues {
  onFormSubmit: any;
}

export default function AppointmentComponent({
  onFormSubmit,
}: AppointmentFormValues) {
  const [name, setName] = useState("");
  const [email, setEmail] = useState("");
  const [mobile, setMobile] = useState("");
  const [doctor, setDoctor] = useState("");
  const [date, setDate] = useState(null);
  const [time, setTime] = useState("");
  const [description, setDescription] = useState("");

  const handleNameChange = (value: string) => {
    setName(value);
  };
  const handleEmailChange = (value: string) => {
    setEmail(value);
  };
  const handleMobileChange = (value: string) => {
    setMobile(value);
  };
  const handleDoctorChange = (value: string) => {
    setDoctor(value);
  };
  const handleDateChange = (value: string) => {
    setDate(value);
  };
  const handleTimeChange = (value: string) => {
    setTime(value);
  };

  const appointment = new Appointment();
  appointment.NAME = name;
  appointment.EMAIL = email;
  appointment.MOBILE = mobile;
  appointment.DOCTOR = doctor;
  appointment.DATE = date;
  appointment.TIME = time;
  appointment.DESCRIPTION = description;

  const handleFormSubmit = () => {
    onFormSubmit(appointment);
  };

  return (
    <section className={styles.appointmentSection}>
      <div className={`${styles.appointmentContainer} container`}>
        <div className={styles.appointmentInfo}>
          <p className="section-title">Appointment</p>
          <p className={styles.appointmentHeading}>
            Make An Appointment To Visit Our Doctors
          </p>
          <p className={styles.appointmentText}>
            Tempor erat elitr rebum at clita. Diam dolor diam ipsum sit. Aliqu
            diam amet diam et eos. Clita erat ipsum et lorem et sit, sed stet
            lorem sit clita duo justo magna dolore erat amet
          </p>
          <div className={styles.callUs}>
            <div className={styles.phoneIcon}>
              <i className="fa fa-phone-alt"></i>
            </div>
            <div className={styles.callUsInfo}>
              <p className={styles.callUsText}>Call Us Now</p>
              <p className={styles.phoneNumber}>+012 345 6789</p>
            </div>
          </div>

          <div className={styles.mailUs}>
            <div className={styles.mailIcon}>
              <i className="fa fa-envelope-open"></i>
            </div>
            <div className={styles.callUsInfo}>
              <p className={styles.callUsText}>Mail Us Now</p>
              <p className={styles.email}>info&#64;example.com</p>
            </div>
          </div>
        </div>
        <div className={styles.appointmentForm}>
          <Input
            inputType={"text"}
            placeholder={"Name"}
            onInputChange={handleNameChange}
            name={"NAME"}
            handleFileInputChange={null}
          />
          <Input
            inputType={"email"}
            placeholder={"Your Email"}
            onInputChange={handleEmailChange}
            name={"EMAIL"}
            handleFileInputChange={null}
          />
          <Input
            inputType={"text"}
            placeholder={"Your Mobile"}
            onInputChange={handleMobileChange}
            name={"MOBILE"}
            handleFileInputChange={null}
          />
          <Input
            inputType={"text"}
            placeholder={"Choose Doctor"}
            onInputChange={handleDoctorChange}
            name={"DOCTOR"}
            handleFileInputChange={null}
          />
          <Input
            inputType={"date"}
            placeholder={"Choose Date"}
            onInputChange={handleDateChange}
            name={"DATE"}
            handleFileInputChange={null}
          />
          <Input
            inputType={"time"}
            placeholder={"Choose Time"}
            onInputChange={handleTimeChange}
            name={"TIME"}
            handleFileInputChange={null}
          />
          <textarea
            cols={30}
            rows={10}
            placeholder="Describe Your Problem"
            name="DESCRIPTION"
            onChange={(e) => setDescription(e.target.value)}
          ></textarea>

          <button
            className={styles.bookBppointmentBtn}
            onClick={handleFormSubmit}
          >
            Book Appointment
          </button>
        </div>
      </div>
    </section>
  );
}
