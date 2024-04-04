import { useState } from "react";
import Input from "../components/Input/Input";
import styles from "./DoctorForm.module.css";
import { useDispatch, useSelector } from "react-redux";
import { AppDispatch, RootState } from "../redux/store";
import {
  CreateDoctorResult,
  Doctor,
  createDoctor,
} from "../redux/DoctorReducer";
import { uploadFile } from "../redux/FileReducer";

export default function DoctorForm() {
  const dispatch = useDispatch<AppDispatch>();

  const token = useSelector((state: RootState) => state.user.token);

  const [name, setName] = useState("");
  const [department, setDepartment] = useState("");
  const [facebookLink, setFacebookLink] = useState("");
  const [twitterLink, setTwitterLink] = useState("");
  const [instagramLink, setInstagramLink] = useState("");
  const [description, setDescription] = useState("");

  const [selectedFile, setSelectedFile] = useState<File | null>(null);

  const handleFileInputChange = (file: File | null) => {
    setSelectedFile(file);
    console.log(selectedFile);
  };

  const handleNameChange = (value: string) => {
    setName(value);
  };
  const handleDepartmentChange = (value: string) => {
    setDepartment(value);
  };
  const handleFacebookLinkChange = (value: string) => {
    setFacebookLink(value);
  };
  const handleTwitterLinkChange = (value: string) => {
    setTwitterLink(value);
  };
  const handleInstagramLinkChange = (value: string) => {
    setInstagramLink(value);
  };
  const handleDescriptionChange = (value: string) => {
    setDescription(value);
  };

  const doctor = new Doctor();
  doctor.DEPARTMENT = department;
  doctor.NAME = name;
  doctor.DESCRIPTION = description;
  doctor.FACEBOOKLINK = facebookLink;
  doctor.TWITTERLINK = twitterLink;
  doctor.INSTAGRAMLINK = instagramLink;

  const handleAddDoctor = async () => {
    try {
      var result = await dispatch(
        createDoctor({ doctor: doctor, token: token })
      );

      var resultBody = result.payload as CreateDoctorResult;

      if (resultBody.IsSuccess === true && selectedFile) {
        const fileResult = await dispatch(
          uploadFile({
            file: selectedFile,
            relKey: resultBody.MyResult.DOCTORID,
            relTable: "DOCTORTABLE",
            relField: "DoctorImage",
          })
        );

        console.log(fileResult);
      }

      //console.log(formData);
      console.log(result.payload as CreateDoctorResult);
    } catch (error) {
      console.error(error);
    }
  };

  return (
    <div className={styles.form}>
      <div className={styles.formContainer}>
        <Input
          inputType={"text"}
          placeholder={"name"}
          name={"NAME"}
          onInputChange={handleNameChange}
          handleFileInputChange={null}
        />
        <Input
          inputType={"text"}
          placeholder={"department"}
          name={"DEPARTMENT"}
          onInputChange={handleDepartmentChange}
          handleFileInputChange={null}
        />
        <Input
          inputType={"text"}
          placeholder={"facebook link"}
          name={"FACEBOOKLINK"}
          onInputChange={handleFacebookLinkChange}
          handleFileInputChange={null}
        />
        <Input
          inputType={"text"}
          placeholder={"twitter link"}
          name={"TWITTERLINK"}
          onInputChange={handleTwitterLinkChange}
          handleFileInputChange={null}
        />
        <Input
          inputType={"text"}
          placeholder={"instagram link"}
          name={"INSTAGRAMLINK"}
          onInputChange={handleInstagramLinkChange}
          handleFileInputChange={null}
        />
        <Input
          inputType={"text"}
          placeholder={"description"}
          name={"DESCRIPTION"}
          onInputChange={handleDescriptionChange}
          handleFileInputChange={null}
        />
        <Input
          inputType={"file"}
          placeholder={"description"}
          name={"DESCRIPTION"}
          onInputChange={null}
          handleFileInputChange={handleFileInputChange}
        />
        <button className={styles.submitBtn} onClick={handleAddDoctor}>
          Add Doctor
        </button>
      </div>
    </div>
  );
}
