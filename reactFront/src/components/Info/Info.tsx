import styles from "./Info.module.css";
import img from "../../assets/carousel-1.jpg";
import { Information } from "../../redux/InfoReducer";

interface InfoProps {
  info: Information;
}

export default function Info({ info }: InfoProps) {
  return (
    <section className={styles.infoSection}>
      <div className={styles.info}>
        <div className={styles.infoPart}>
          <div className={styles.infoHeader}>
            <p>Good Health Is The Root Of All Heppiness</p>
          </div>
          <div className={styles.infoStatistics}>
            <div className={styles.statisticsContainer}>
              <div className={styles.statistics}>
                <p className={styles.number}>{info.DOCTORS}</p>
                <p className={styles.text}>Expert Doctors</p>
              </div>
              <div className={styles.statistics}>
                <p className={styles.number}>{info.STAFF}</p>
                <p className={styles.text}>Medical Staff</p>
              </div>
              <div className={styles.statistics}>
                <p className={styles.number}>{info.PATIENTS}</p>
                <p className={styles.text}>Total Patients</p>
              </div>
            </div>
          </div>
        </div>
        <div className={styles.infoCarousel}>
          <div className={styles.carousel}>
            <h1>Cardiology</h1>
            <img src={img} alt="" className={styles.carouselImg} />
          </div>
        </div>
      </div>
    </section>
  );
}
