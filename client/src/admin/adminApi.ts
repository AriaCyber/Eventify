import axios from "axios";

const api = axios.create({
  baseURL: "https://localhost:5001/api", // adjust if needed
});

// EVENTS
export const getAdminEvents = () =>
  api.get("/admin/events").then(r => r.data);

export const createEvent = (ev:any) =>
  api.post("/admin/events", ev).then(r => r.data);

export const deleteEvent = (id:number) =>
  api.delete(`/admin/events/${id}`);

// REFUNDS
export const getRefunds = () =>
  api.get("/admin/refunds").then(r => r.data);

export const updateRefundStatus = (id:number, status:string) =>
  api.put(`/admin/refunds/${id}/status`, status, {
    headers: { "Content-Type": "application/json" }
  });

// PROMO CODES
export const getPromoCodes = () =>
  api.get("/admin/promocodes").then(r => r.data);

export const createPromoCode = (dto:any) =>
  api.post("/admin/promocodes", dto).then(r => r.data);

// REPORTS
export const getSalesReport = () =>
  api.get("/reports/sales").then(r => r.data);

export const getOccupancyReport = () =>
  api.get("/reports/occupancy").then(r => r.data);
