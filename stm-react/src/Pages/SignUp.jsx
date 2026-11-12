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

import { registerUser } from "../services/authService";

function SignUp() {
  const navigate = useNavigate();

  const [formData, setFormData] = useState({
    firstName: "",
    lastName: "",
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
      await registerUser(formData);

      navigate("/signin", {
        state: {
          message: "Registration successful. Please sign in.",
        },
      });
    } catch (err) {
      const data = err.response?.data;

      setError(
        data?.message ||
          data?.title ||
          err.message ||
          "Registration failed."
      );
    } finally {
      setLoading(false);
    }
  };

  return (
    <Container maxWidth="sm">
      <Box sx={{ mt: 6, mb: 6 }}>
        <Paper elevation={3} sx={{ p: 4, borderRadius: 3 }}>
          <Stack spacing={3}>
            <Typography variant="h4" fontWeight="bold" align="center">
              Create Account
            </Typography>

            {error && <Alert severity="error">{error}</Alert>}

            <Box component="form" onSubmit={handleSubmit}>
              <Stack spacing={2}>
                <TextField
                  label="First Name"
                  name="firstName"
                  required
                  fullWidth
                  value={formData.firstName}
                  onChange={handleChange}
                />

                <TextField
                  label="Last Name"
                  name="lastName"
                  required
                  fullWidth
                  value={formData.lastName}
                  onChange={handleChange}
                />

                <TextField
                  label="Email"
                  name="email"
                  type="email"
                  required
                  fullWidth
                  value={formData.email}
                  onChange={handleChange}
                />

                <TextField
                  label="Password"
                  name="password"
                  type="password"
                  required
                  fullWidth
                  value={formData.password}
                  onChange={handleChange}
                />

                <Button
                  type="submit"
                  variant="contained"
                  size="large"
                  disabled={loading}
                >
                  {loading ? "Creating Account..." : "Sign Up"}
                </Button>
              </Stack>
            </Box>

            <Typography align="center">
              Already have an account?{" "}
              <Link to="/signin">Sign In</Link>
            </Typography>
          </Stack>
        </Paper>
      </Box>
    </Container>
  );
}

export default SignUp;