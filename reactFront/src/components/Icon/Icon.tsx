import styles from "./Icon.module.css";

interface IconProps {
  iconContainerSize: string;
  cursorPointer: string;
  icon: string;
  iconSize: string;
}

export default function Icon({
  iconContainerSize,
  cursorPointer,
  icon,
  iconSize,
}: IconProps) {
  return (
    <div
      className={`${styles.iconContainer} ${styles[iconContainerSize]} ${styles[cursorPointer]}`}
    >
      <i className={`${icon} ${styles[iconSize]}`}></i>
    </div>
  );
}
