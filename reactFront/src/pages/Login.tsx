import { useState } from "react";
import Input from "../components/Input/Input";
import styles from "./Login.module.css";
import { GetLoginResult, Login, loginUser } from "../redux/UserReducer";
import { useDispatch } from "react-redux";
import { AppDispatch } from "../redux/store";
import { useNavigate } from "react-router-dom";
export default function LoginComponent() {
  const [userName, setUserName] = useState("");
  const [password, setPassword] = useState("");

  const navigate = useNavigate();

  const dispatch = useDispatch<AppDispatch>();

  const handleUserName = (value: string) => {
    setUserName(value);
  };
  const handlePassword = (value: string) => {
    setPassword(value);
  };

  const login = new Login();
  login.PASSWORD = password;
  login.USERNAME = userName;

  const handleLogin = async (user: Login) => {
    const result = await dispatch(loginUser(user));
    const payload = result.payload as GetLoginResult;
    if (payload.IsSuccess && payload.ResultCode === 200) {
      navigate("/");
    }
  };

  return (
    <div className={styles.loginForm}>
      <Input
        inputType={"text"}
        placeholder={"enter username"}
        name={"username"}
        onInputChange={handleUserName}
        handleFileInputChange={function (file: File): void {}}
      />
      <Input
        inputType={"text"}
        placeholder={"enter password"}
        name={"password"}
        onInputChange={handlePassword}
        handleFileInputChange={function (file: File): void {}}
      />
      <button className="login" onClick={() => handleLogin(login)}>
        Login
      </button>
    </div>
  );
}
