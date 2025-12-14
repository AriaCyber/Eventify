import { http } from "./http";
import type { BookingRequest } from "../types";

export const checkout = (req: BookingRequest) =>
  http.post("/api/bookings", req).then(r => r.data);

export const myTickets = (email: string) =>
  http.get(`/api/bookings/by-email/${encodeURIComponent(email)}`).then(r => r.data);

export const requestRefund = (orderId: number, reason: string) =>
  http.post("/api/refunds", { orderId, reason }).then(r => r.data);
