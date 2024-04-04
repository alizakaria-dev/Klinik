import { useParams } from "react-router-dom";
import Icon from "../components/Icon/Icon";
import styles from "./DoctorDetails.module.css";
import { useDispatch, useSelector } from "react-redux";
import { AppDispatch, RootState } from "../redux/store";
import { useEffect, useState } from "react";
import {
  Doctor,
  GetDoctorResult,
  UpdateDoctorResult,
  getDoctor,
  updateDoctor,
} from "../redux/DoctorReducer";
import notFound from "../assets/image-not-found.jpg";
import { deleteFile, uploadFile } from "../redux/FileReducer";
import { GetTokenFromLocalStorage, setToken } from "../redux/UserReducer";

export default function DoctorDetails() {
  const { doctorId } = useParams();

  const authToken = useSelector((state: RootState) => state.user.token);

  useEffect(() => {
    const localStorageValue = GetTokenFromLocalStorage();

    if (localStorageValue) {
      dispatch(setToken(localStorageValue));
    }
  }, []);

  const dispatch = useDispatch<AppDispatch>();

  const doctor = useSelector(
    (state: RootState) => state.doctor.data as GetDoctorResult
  );

  const [toggleMode, setToggleMode] = useState<boolean>(false);

  const [name, setName] = useState("");
  const [department, setDepartment] = useState("");
  const [description, setDescription] = useState("");

  const [selectedFile, setSelectedFile] = useState(null);

  const handleToggleMode = () => {
    setToggleMode(!toggleMode);
  };

  const handleDoctorUpdate = async (updatedDoctor: Doctor) => {
    try {
      var updateDoctorResult = await dispatch(
        updateDoctor({ doctor: updatedDoctor, token: authToken })
      );

      var updateDoctorBody = updateDoctorResult.payload as UpdateDoctorResult;

      if (updateDoctorBody.IsSuccess === true && selectedFile) {
        const fileResult = await dispatch(
          uploadFile({
            file: selectedFile,
            relKey: doctor.MyResult.DOCTORID,
            relTable: "DOCTORTABLE",
            relField: "DoctorImage",
          })
        );

        console.log(fileResult);
      }
    } catch (error) {
      console.error(error);
    }
  };

  const deleteImage = async (id: number) => {
    var result = await dispatch(deleteFile({ id, token: authToken }));
    console.log(result);
  };

  useEffect(() => {
    const fetchData = async () => {
      try {
        await dispatch(getDoctor(parseInt(doctorId)));
      } catch (error) {
        console.error("Error fetching doctor:", error);
      }
    };

    fetchData();
  }, []);

  const doctorForm = new Doctor();
  doctorForm.DOCTORID = +doctorId;
  doctorForm.DEPARTMENT = department;
  doctorForm.NAME = name;
  doctorForm.DESCRIPTION = description;
  doctorForm.TWITTERLINK = doctor?.MyResult.TWITTERLINK;
  doctorForm.FACEBOOKLINK = doctor?.MyResult.FACEBOOKLINK;
  doctorForm.INSTAGRAMLINK = doctor?.MyResult.INSTAGRAMLINK;

  useEffect(() => {
    setName(doctor?.MyResult.NAME || "");
    setDescription(doctor?.MyResult.DESCRIPTION || "");
    setDepartment(doctor?.MyResult.DEPARTMENT || "");
  }, [doctor]);

  if (doctor) {
    if (!toggleMode) {
      return (
        <div className={`${styles.doctorDetails} container`}>
          <div className={styles.image}>
            {doctor.MyResult.Files ? (
              <img src={doctor.MyResult.Files[0].URL} alt="" />
            ) : (
              <img src={notFound} alt="" />
            )}
          </div>
          <div className={styles.details}>
            <h1>{doctor.MyResult.NAME}</h1>
            <h3>{doctor.MyResult.DEPARTMENT}</h3>
            <p>{doctor.MyResult.DESCRIPTION}</p>
            <div className={styles.icons}>
              <Icon
                iconContainerSize={"iconContainerMd"}
                cursorPointer={"pointerCursor"}
                icon={"fab fa-facebook-f"}
                iconSize={"iconMd"}
              />
              <Icon
                iconContainerSize={"iconContainerMd"}
                cursorPointer={"pointerCursor"}
                icon={"fab fa-twitter"}
                iconSize={"iconMd"}
              />
              <Icon
                iconContainerSize={"iconContainerMd"}
                cursorPointer={"pointerCursor"}
                icon={"fab fa-instagram"}
                iconSize={"iconMd"}
              />
              <button className={styles.toggleBtn} onClick={handleToggleMode}>
                Edit
              </button>
            </div>
          </div>
        </div>
      );
    } else {
      return (
        <div className={`${styles.doctorDetails} container`}>
          <div className={styles.image}>
            {doctor?.MyResult?.Files ? (
              <img src={doctor.MyResult.Files[0].URL} alt="" />
            ) : (
              <img src={notFound} alt="" />
            )}
            <div>
              <button
                className={styles.deleteBtn}
                onClick={() => deleteImage(doctor.MyResult.Files[0].FILEID)}
              >
                X
              </button>
            </div>
          </div>
          <div className={styles.details}>
            <input
              type="text"
              name=""
              id=""
              value={name}
              onChange={(e) => setName(e.target.value)}
            />
            <input
              type="text"
              name=""
              id=""
              value={department}
              onChange={(e) => setDepartment(e.target.value)}
            />
            <input
              type="text"
              name=""
              id=""
              value={description}
              onChange={(e) => setDescription(e.target.value)}
            />
            <input
              type="file"
              name=""
              id=""
              onChange={(e) => setSelectedFile(e.target.files[0])}
            />
            <div className={styles.icons}>
              <Icon
                iconContainerSize={"iconContainerMd"}
                cursorPointer={"pointerCursor"}
                icon={"fab fa-facebook-f"}
                iconSize={"iconMd"}
              />
              <Icon
                iconContainerSize={"iconContainerMd"}
                cursorPointer={"pointerCursor"}
                icon={"fab fa-twitter"}
                iconSize={"iconMd"}
              />
              <Icon
                iconContainerSize={"iconContainerMd"}
                cursorPointer={"pointerCursor"}
                icon={"fab fa-instagram"}
                iconSize={"iconMd"}
              />
              <button
                className={styles.toggleBtn}
                onClick={() => handleDoctorUpdate(doctorForm)}
              >
                Update
              </button>
            </div>
          </div>
        </div>
      );
    }
  } else {
    return null;
  }
}
