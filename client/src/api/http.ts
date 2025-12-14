import axios from "axios";
export const http = axios.create({
  baseURL: import.meta.env.VITE_API_BASE || "http://localhost:5110",
  timeout: 10000,
});
