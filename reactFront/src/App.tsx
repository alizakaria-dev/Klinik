import { Outlet, RouterProvider, createBrowserRouter } from "react-router-dom";
import Appointments from "./pages/Appointments";
import DoctorDetails from "./pages/DoctorDetails";
import DoctorForm from "./pages/DoctorForm";
import Home from "./pages/Home";
import LoginComponent from "./pages/Login";
import ProtectedRoute from "./components/ProtectedRoute";
import Nav from "./components/Navbar/Nav";
import { useEffect } from "react";
import { useDispatch, useSelector } from "react-redux";
import {
  GetTokenFromLocalStorage,
  setToken,
  getUserRole,
  checkLocalStorageValue,
} from "./redux/UserReducer";
import { AppDispatch, RootState } from "./redux/store";

const router = createBrowserRouter([
  {
    path: "/",
    element: <RootLayout />,
    children: [
      {
        path: "/",
        element: <Home />,
        index: true,
      },
      {
        path: "login",
        element: <LoginComponent />,
      },
      {
        path: "doctor-form",
        element: (
          <ProtectedRoute>
            <DoctorForm />
          </ProtectedRoute>
        ),
      },
      {
        path: "doctor-details/:doctorId",
        element: <DoctorDetails />,
      },
      {
        path: "appointments",
        element: <Appointments />,
      },
    ],
  },
]);

function App() {
  return <RouterProvider router={router} />;
}

function RootLayout() {
  const dispatch = useDispatch<AppDispatch>();
  const userState = useSelector((state: RootState) => state.user);

  useEffect(() => {
    const localStorageValue = GetTokenFromLocalStorage();

    if (localStorageValue) {
      dispatch(setToken(localStorageValue));

      dispatch(getUserRole(localStorageValue));
    }

    dispatch(checkLocalStorageValue());
  }, []);
  return (
    <div>
      <Nav localStorageValue={userState.token} roleId={userState.role} />
      <Outlet />
    </div>
  );
}

export default App;
