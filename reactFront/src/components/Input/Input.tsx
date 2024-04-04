import { useState } from "react";
import styles from "./Input.module.css";

interface InputProps {
  inputType: string;
  placeholder: string;
  name: string;
  onInputChange: (value: string) => void;
  handleFileInputChange: (file: File | null) => void;
}

export default function Input({
  inputType,
  placeholder,
  name,
  onInputChange,
  handleFileInputChange,
}: InputProps) {
  const [inputValue, setInputValue] = useState("");

  const handleChange = (e: React.ChangeEvent<HTMLInputElement>) => {
    const value = e.target.value;
    setInputValue(value);
    onInputChange(value);
  };

  const handleInputChange = (event: React.ChangeEvent<HTMLInputElement>) => {
    const file = event.target.files ? event.target.files[0] : null;
    handleFileInputChange(file);
  };

  if (inputType != "file") {
    return (
      <input
        className={styles.input}
        type={inputType}
        placeholder={placeholder}
        name={name}
        value={inputValue}
        onChange={handleChange}
      />
    );
  } else {
    return (
      <input
        className={styles.input}
        type={inputType}
        placeholder={placeholder}
        name={name}
        value={inputValue}
        onChange={handleInputChange}
      />
    );
  }
}
