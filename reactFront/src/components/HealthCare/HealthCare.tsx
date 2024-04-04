import { Service } from "../../redux/HealthCareReducer";
import Icon from "../Icon/Icon";
import styles from "./HealthCare.module.css";

interface ServiceProps {
  services: Service[];
}

export default function HealthCare({ services }: ServiceProps) {
  return (
    <section className={styles.healthcareSection}>
      <div className={styles.titleContainer}>
        <p className="section-title">Services</p>
        <h1 className={styles.servicesHeading}>Health Care Solutions</h1>
      </div>

      <div className={`${styles.services} container`}>
        {services.map((service) => (
          <div className={styles.service} key={service.SERVICEID}>
            <Icon
              iconContainerSize={"iconContainerLg"}
              cursorPointer={""}
              icon={service.ICON}
              iconSize={"iconLg"}
            />
            <h4>{service.TITLE}</h4>
            <p>{service.TEXT}</p>
            <div className={styles.readMore}>
              <div className={styles.plusSign}>
                <i className="fa fa-plus"></i>
              </div>
              <p className={styles.readMoreText}>Read More</p>
            </div>
          </div>
        ))}
      </div>
    </section>
  );
}
