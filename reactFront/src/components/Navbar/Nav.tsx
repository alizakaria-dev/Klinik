import { Link } from "react-router-dom";
import styles from "./Nav.module.css";
import { useDispatch } from "react-redux";
import { clearToken } from "../../redux/UserReducer";
import { AppDispatch } from "../../redux/store";
import { useState } from "react";

interface NavProps {
  localStorageValue: string;
  roleId: number;
}

export default function Nav({ localStorageValue, roleId }: NavProps) {
  const [isNavToggled, setIsNavToggled] = useState(false);

  const dispatch = useDispatch<AppDispatch>();

  const handleLogout = () => {
    dispatch(clearToken());
  };

  const handleIsNavToggled = () => {
    setIsNavToggled(!isNavToggled);
  };

  return (
    <section>
      <nav>
        <div className={styles.logo}>
          <i className="far fa-hospital"></i>
          <h1 className={styles.logoHeader}>Klinik</h1>
        </div>
        <div className={styles.navItems}>
          <ul
            className={`${styles.navList} ${isNavToggled ? styles.showNavItems : ""}`}
          >
            <li className={styles.navItem}>
              <Link to={"/"} className={styles.link}>
                Home
              </Link>
            </li>

            {roleId && roleId == 3 ? (
              <li className={styles.navItem}>
                <Link to={"appointments"} className={styles.link}>
                  Appointments
                </Link>
              </li>
            ) : null}

            <li className={styles.navItem}>SERVICE</li>

            {roleId && roleId == 3 ? (
              <li className={styles.navItem}>
                <Link to={"doctor-form"} className={styles.link}>
                  Add Doctor
                </Link>
              </li>
            ) : null}

            <li className={styles.navItem}>Add Testimonial</li>
            <li className={styles.navItem}>CONTACT</li>
          </ul>

          {localStorageValue ? (
            <div
              className={`${styles.navItem} ${styles.appointmentBtn}`}
              onClick={handleLogout}
            >
              <span>Logout</span>
            </div>
          ) : (
            <Link to={"login"}>
              <div className={`${styles.navItem} ${styles.appointmentBtn}`}>
                <span>Login</span>
              </div>
            </Link>
          )}

          <div className={styles.burgerIcon} onClick={handleIsNavToggled}>
            <i className="fa-solid fa-bars"></i>
          </div>
        </div>
      </nav>
    </section>
  );
}
