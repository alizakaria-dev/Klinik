import { useState } from "react";
import { Testimonial } from "../../redux/TestimonialReducer";
import styles from "./Testimonial.module.css";

interface TestimonialProps {
  testimonials: Testimonial[];
}

export default function TestimonialComponent({
  testimonials,
}: TestimonialProps) {
  const initialIndex = Math.floor(testimonials.length / 2);

  const [selectedIndex, setSelectedIndex] = useState(initialIndex);
  const [animationDirection, setAnimationDirection] = useState("");

  const onPrevClick = (currentIndex: number) => {
    if (currentIndex === 0) {
      setSelectedIndex(testimonials.length - 1);
      setAnimationDirection(styles.moveLeft);
    } else {
      setSelectedIndex(currentIndex - 1);
      setAnimationDirection(styles.moveLeft);
    }
  };

  const onNextClick = (currentIndex: number) => {
    if (currentIndex === testimonials.length - 1) {
      setSelectedIndex(0);
      setAnimationDirection(styles.moveRight);
    } else {
      setSelectedIndex(currentIndex + 1);
      setAnimationDirection(styles.moveRight);
    }
  };

  return (
    <section className={`${styles.testimonialSection}`}>
      <div className={`${styles.testimonialContainer} container`}>
        <p className="section-title">Testimonial</p>
        <h1 className={styles.testimonialHeading}>What Our Patients Say</h1>
        <div className={styles.flexContainer}>
          {testimonials.map((testimonial, index) => (
            <div
              className={`${styles.testimonialCard} ${selectedIndex === index ? styles.active : ""} ${animationDirection}`}
              key={index}
            >
              <div className={styles.upperSection}>
                <i
                  className={`fa-solid fa-arrow-left ${styles.iconLeft} ${styles.icon}`}
                  onClick={() => onPrevClick(index)}
                ></i>
                <div className={styles.patientImage}>
                  <img src={testimonial.File.URL} alt="" />
                </div>
                <i
                  className={`fa-solid fa-arrow-right ${styles.iconRight} ${styles.icon}`}
                  onClick={() => onNextClick(index)}
                ></i>
              </div>
              <div className={styles.textSection}>
                <div className={styles.text}>
                  <p>{testimonial.DESCRIPTION}</p>
                  <p className={styles.name}>{testimonial.USERNAME}</p>
                  <p className={styles.profession}>{testimonial.PROFESSION}</p>
                </div>
              </div>
            </div>
          ))}
        </div>
      </div>
    </section>
  );
}
