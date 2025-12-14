export function eventDate(e: any): string {
  const raw =
    e?.dateTime ??
    e?.scheduleDateTime ??
    e?.scheduledDateTime ??
    e?.date ??
    "";
  const d = new Date(raw);
  return isNaN(d.getTime()) ? "TBD" : d.toLocaleString();
}

export function eventTiers(detail: any) {
  return detail?.tiers ?? detail?.ticketTiers ?? [];
}
