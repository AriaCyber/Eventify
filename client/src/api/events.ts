import { http } from "./http";
export const getEvents = (params?: Record<string,string|number|boolean>) =>
  http.get("/api/events", { params }).then(r => r.data);
export const getEvent = (id: number) =>
  http.get(`/api/events/${id}`).then(r => r.data);
