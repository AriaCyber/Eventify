export type EventSummary = {
  id: number;
  title: string;
  dateTime?: string;
  scheduleDateTime?: string;
  scheduledDateTime?: string;
  date?: string;
  venue?: string;
  isPublic?: boolean;
  lowestPrice?: number;
  imageUrl?: string;
};

export type EventDetail = EventSummary & {
  description?: string;
  
  tiers?: { id: number; name: string; price: number; capacity: number; sold: number }[];
  ticketTiers?: { id: number; name: string; price: number; capacity: number; sold: number }[];
};
export type BookingRequest = { eventId: number; tierId: number; quantity: number; email: string; promoCode?: string; };
export type BookingResponse = { orderId: number; ticketSerials: string[]; total?: number };
export type Review = { id: number; eventId: number; rating: number; comment: string; createdAt: string; user?: string };


