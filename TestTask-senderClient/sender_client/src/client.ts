import axios from "axios";

const client = axios.create({
  baseURL: "https://localhost:7133/api/Messages",
  timeout: 1000,
});

export default client;
