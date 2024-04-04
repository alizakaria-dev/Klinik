//import "@fortawesome/fontawesome-free/css/all.min.css";
import styles from "./About.module.css";
import imgOne from "../../assets/about-1.jpg";
import imgTwo from "../../assets/about-2.jpg";

export default function About() {
  return (
    <section className={styles.aboutusSection}>
      <div className={`${styles.aboutusContainer} container`}>
        <div className={styles.aboutusImages}>
          <img src={imgOne} alt="" className={styles.firstImg} />
          <img src={imgTwo} alt="" className={styles.secondImg} />
        </div>
        <div className={styles.aboutus}>
          <p className="section-title">About Us</p>
          <h1 className={styles.aboutusHeading}>
            Why You Should Trust Us ? Know About Us !
          </h1>
          <p className={styles.aboutusInfo}>
            Tempor erat elitr rebum at clita. Diam dolor diam ipsum sit. Aliqu
            diam amet diam et eos. Clita erat ipsum et lorem et sit, sed stet
            lorem sit clita duo justo magna dolore erat amet
            <br />
            <br />
            Stet no et lorem dolor et diam, amet duo ut dolore vero eos. No stet
            est diam rebum amet diam ipsum. Clita clita labore, dolor duo nonumy
            clita sit at, sed sit sanctus dolor eos.
          </p>
          <ul className={styles.featuresList}>
            <li className={styles.featureItem}>
              <div className={styles.icon}>
                <i className="far fa-check-circle"></i>
              </div>
              <div className={styles.featureText}>
                <p>Quality Health Care</p>
              </div>
            </li>
            <li className={styles.featureItem}>
              <div className={styles.icon}>
                <i className="far fa-check-circle"></i>
              </div>
              <div className={styles.featureText}>
                <p>Only Qualified Doctors</p>
              </div>
            </li>
            <li className={styles.featureItem}>
              <div className={styles.icon}>
                <i className="far fa-check-circle"></i>
              </div>
              <div className={styles.featureText}>
                <p>Medical Research Professionals</p>
              </div>
            </li>
          </ul>

          <button className={styles.aboutUsBtn}>Read More</button>
        </div>
      </div>
    </section>
  );
}
