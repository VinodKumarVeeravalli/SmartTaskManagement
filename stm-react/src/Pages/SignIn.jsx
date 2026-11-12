import { useState } from "react";
import { Link, useNavigate } from "react-router-dom";
import {
  Box,
  Button,
  Container,
  Paper,
  TextField,
  Typography,
  Alert,
  Stack,
} from "@mui/material";

import { loginUser } from "../services/authService";

function SignIn() {
  const navigate = useNavigate();

  const [formData, setFormData] = useState({
    email: "",
    password: "",
  });

  const [error, setError] = useState("");
  const [loading, setLoading] = useState(false);

  const handleChange = (event) => {
    const { name, value } = event.target;

    setFormData((previous) => ({
      ...previous,
      [name]: value,
    }));
  };

  const handleSubmit = async (event) => {
    event.preventDefault();
    setError("");
    setLoading(true);

    try {
      const response = await loginUser(formData);

      // Adjust this property to match your backend LoginResponseDto.
      const token = response.token ?? response.accessToken;

      if (!token) {
        throw new Error("Login response did not contain a token.");
      }

      // Temporary learning implementation:
      // JWT remains in memory until the page is refreshed.
      window.stmAccessToken = token;

      navigate("/dashboard");
    } catch (err) {
      setError(
        err.response?.data?.message ||
          err.response?.data?.title ||
          err.message ||
          "Unable to sign in."
      );
    } finally {
      setLoading(false);
    }
  };

  return (
    <Container maxWidth="sm">
      <Box sx={{ mt: 10 }}>
        <Paper elevation={3} sx={{ p: 4, borderRadius: 3 }}>
          <Stack spacing={3}>
            <Typography variant="h4" fontWeight="bold" align="center">
              Smart Task Management
            </Typography>

            <Typography variant="h6" align="center">
              Sign In
            </Typography>

            {error && <Alert severity="error">{error}</Alert>}

            <Box component="form" onSubmit={handleSubmit}>
              <Stack spacing={3}>
                <TextField
                  label="Email"
                  name="email"
                  type="email"
                  fullWidth
                  required
                  value={formData.email}
                  onChange={handleChange}
                />

                <TextField
                  label="Password"
                  name="password"
                  type="password"
                  fullWidth
                  required
                  value={formData.password}
                  onChange={handleChange}
                />

                <Button
                  type="submit"
                  variant="contained"
                  size="large"
                  disabled={loading}
                >
                  {loading ? "Signing In..." : "Sign In"}
                </Button>
              </Stack>
            </Box>

            <Typography align="center">
              Don't have an account?{" "}
              <Link to="/signup">Sign Up</Link>
            </Typography>
          </Stack>
        </Paper>
      </Box>
    </Container>
  );
}

export default SignIn;