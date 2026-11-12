import { useNavigate } from "react-router-dom";
import {
  Container,
  Typography,
  Paper,
  Button,
  Stack,
} from "@mui/material";

function Dashboard() {
  const navigate = useNavigate();

  const handleLogout = () => {
    window.stmAccessToken = null;
    navigate("/signin", { replace: true });
  };

  return (
    <Container maxWidth="lg" sx={{ mt: 5 }}>
      <Paper sx={{ p: 4 }}>
        <Stack spacing={3}>
          <Typography variant="h4">
            Smart Task Management Dashboard
          </Typography>

          <Typography>
            Welcome! You have successfully signed in.
          </Typography>

          <Button
            variant="outlined"
            onClick={handleLogout}
            sx={{ alignSelf: "flex-start" }}
          >
            Logout
          </Button>
        </Stack>
      </Paper>
    </Container>
  );
}

export default Dashboard;