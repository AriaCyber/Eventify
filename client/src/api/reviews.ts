import { http } from "./http";
export const listReviews = (eventId: number) =>
  http.get("/api/reviews", { params: { eventId } }).then(r => r.data);

export const addReview = (eventId: number, rating: number, comment: string, email: string) =>
  http.post("/api/reviews", { eventId, rating, comment, email }).then(r => r.data);
