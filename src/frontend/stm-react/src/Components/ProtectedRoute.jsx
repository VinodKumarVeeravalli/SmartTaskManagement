import { Navigate, Outlet } from "react-router-dom";

function ProtectedRoute() {
  const token = window.stmAccessToken;

  if (!token) {
    return <Navigate to="/signin" replace />;
  }

  return <Outlet />;
}

export default ProtectedRoute;