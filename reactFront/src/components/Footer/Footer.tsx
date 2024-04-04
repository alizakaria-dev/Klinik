import styles from "./Footer.module.css";

export default function Footer() {
  return (
    <footer>
      <div className={`${styles.footerContainer} container`}>
        <div className={`${styles.address} ${styles.sectionStyling}`}>
          <h1 className={styles.footSectionTitle}>Address</h1>
          <ul className={styles.sectionList}>
            <li className={styles.sectionItem}>
              <i className="fa fa-map-marker-alt"></i>123 Street, New York, USA
            </li>
            <li className={styles.sectionItem}>
              <i className="fa fa-phone-alt"></i>+123 456 7890
            </li>
            <li className={styles.sectionItem}>
              <i className="fa fa-envelope"></i>info&#64;example.com
            </li>
          </ul>

          <ul className={styles.sectionIcons}>
            <li className={styles.footerIconItem}>
              <i className="fab fa-twitter"></i>
            </li>
            <li className={styles.footerIconItem}>
              <i className="fab fa-facebook-f"></i>
            </li>
            <li className={styles.footerIconItem}>
              <i className="fab fa-youtube"></i>
            </li>
            <li className={styles.footerIconItem}>
              <i className="fab fa-linkedin-in"></i>
            </li>
          </ul>
        </div>
        <div className={`${styles.footerServices} ${styles.sectionStyling}`}>
          <h1 className={styles.footSectionTitle}>Services</h1>
          <ul className={styles.sectionList}>
            <li className={`${styles.sectionItem} ${styles.footerServiceItem}`}>
              <i className="fa-solid fa-chevron-right"></i>Cardiology
            </li>
            <li className={`${styles.sectionItem} ${styles.footerServiceItem}`}>
              <i className="fa-solid fa-chevron-right"></i>Pulmonary
            </li>
            <li className={`${styles.sectionItem} ${styles.footerServiceItem}`}>
              <i className="fa-solid fa-chevron-right"></i>Neurology
            </li>
            <li className={`${styles.sectionItem} ${styles.footerServiceItem}`}>
              <i className="fa-solid fa-chevron-right"></i>Orthopedics
            </li>
            <li className={`${styles.sectionItem} ${styles.footerServiceItem}`}>
              <i className="fa-solid fa-chevron-right"></i>Laboratory
            </li>
          </ul>
        </div>
        <div className={`quick-links ${styles.sectionStyling}`}>
          <h1 className={styles.footSectionTitle}>Quick Links</h1>
          <ul className={styles.sectionList}>
            <li className={`${styles.sectionItem} ${styles.footerServiceItem}`}>
              <i className="fa-solid fa-chevron-right"></i>About us
            </li>
            <li className={`${styles.sectionItem} ${styles.footerServiceItem}`}>
              <i className="fa-solid fa-chevron-right"></i>Contact us
            </li>
            <li className={`${styles.sectionItem} ${styles.footerServiceItem}`}>
              <i className="fa-solid fa-chevron-right"></i>Our Services
            </li>
            <li className={`${styles.sectionItem} ${styles.footerServiceItem}`}>
              <i className="fa-solid fa-chevron-right"></i>Terms & Conditions
            </li>
            <li className={`${styles.sectionItem} ${styles.footerServiceItem}`}>
              <i className="fa-solid fa-chevron-right"></i>Support
            </li>
          </ul>
        </div>
        <div className={`${styles.newsletter} ${styles.sectionStyling}`}>
          <h1 className={styles.footSectionTitle}>News Letter</h1>
          <p>Dolor amet sit justo amet elitr clita ipsum elitr est.</p>
        </div>
      </div>
    </footer>
  );
}
