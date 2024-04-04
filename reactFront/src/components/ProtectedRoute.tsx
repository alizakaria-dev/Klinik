import { useSelector } from "react-redux";
import { RootState } from "../redux/store";
import { Navigate } from "react-router-dom";
import { ReactNode } from "react";

interface ProtectedRouteProps {
  children: ReactNode;
}

export default function ProtectedRoute({ children }: ProtectedRouteProps) {
  const isUserAuthenticated = useSelector(
    (state: RootState) => state.user.isAuthenticated
  );

  return isUserAuthenticated ? children : <Navigate to="/" />;
}
