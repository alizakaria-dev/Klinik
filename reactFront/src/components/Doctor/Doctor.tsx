import { Link } from "react-router-dom";
import { Doctor } from "../../redux/DoctorReducer";
import styles from "./Doctor.module.css";

interface DoctorsProps {
  doctors: Doctor[];
  role: string;
  handleDoctorId: (id: number) => void;
}

export default function DoctorComponent({
  doctors,
  role,
  handleDoctorId,
}: DoctorsProps) {
  return (
    <section className={styles.doctorsSection}>
      <div className={styles.doctorsContainer}>
        <div className={styles.sectionHeading}>
          <p className="section-title">Doctors</p>
          <p className={styles.doctorsHeading}>Our Experienced Doctors</p>
        </div>
        <div className={`${styles.doctors} container`}>
          {doctors && doctors.length > 0
            ? doctors.map((doctor) => (
                <div className={styles.doctor} key={doctor.DOCTORID}>
                  {role === "3" ? (
                    <button
                      className={styles.deleteBtn}
                      onClick={() => handleDoctorId(doctor.DOCTORID)}
                    >
                      X
                    </button>
                  ) : null}

                  <Link to={`doctor-details/${doctor.DOCTORID}`}>
                    <div className={styles.doctorImg}>
                      {doctor.Files.length > 0 ? (
                        <img src={doctor.Files[0].URL} alt="" />
                      ) : null}
                    </div>
                  </Link>
                  <div className={styles.doctorInfo}>
                    <h5>{doctor.NAME}</h5>
                    <p>{doctor.DEPARTMENT}</p>
                    <ul className={styles.socialMediaList}>
                      <li className={styles.listIcons}>
                        <i className="fab fa-facebook-f"></i>
                      </li>
                      <li className={styles.listIcons}>
                        <i className="fab fa-twitter"></i>
                      </li>
                      <li className={styles.listIcons}>
                        <i className="fab fa-instagram"></i>
                      </li>
                    </ul>
                  </div>
                </div>
              ))
            : null}
        </div>
      </div>
    </section>
  );
}
