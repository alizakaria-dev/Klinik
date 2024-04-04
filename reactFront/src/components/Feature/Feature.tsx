import Icon from "../Icon/Icon";
import styles from "./Feature.module.css";
import img from "../../assets/feature.jpg";
import { Feature } from "../../redux/FeatureReducer";

interface FeatureProps {
  feature: Feature;
}

export default function FeatureComponent({ feature }: FeatureProps) {
  return (
    <section className={styles.featuresSection}>
      <div className={styles.featuresContainer}>
        <div className={styles.featuresTexts}>
          <p className={styles.featuresTitle}>Features</p>
          <h1 className={styles.featuresHeading}>{feature.TITLE}</h1>
          <p className={styles.featuresInfo}>{feature.TEXT}</p>
          <div className={styles.featureIcons}>
            {feature.FEATUREITEMS.map((f) => (
              <div className={styles.iconItem} key={f.FEATUREITEMID}>
                <Icon
                  iconContainerSize={"iconContainerMd"}
                  cursorPointer={""}
                  icon={f.ICON}
                  iconSize={"iconMd"}
                />
                <div className={styles.iconTexts}>
                  <p className={styles.textOne}>{f.TEXTONE}</p>
                  <p className={styles.textTwo}>{f.TEXTTWO}</p>
                </div>
              </div>
            ))}
          </div>
        </div>
        <div className={styles.imgContainer}>
          <img src={img} alt="" />
        </div>
      </div>
    </section>
  );
}
