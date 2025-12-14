import { useEffect, useState } from "react";
import { Link } from "react-router-dom";
import { getEvents } from "../api/events";
import type { EventSummary } from "../types";
import { eventDate } from "../utils";

export default function Discover() {
  const [events, setEvents] = useState<EventSummary[]>([]);
  const [q, setQ] = useState("");
  const [loading, setLoading] = useState(false);
  const [err, setErr] = useState("");

  useEffect(() => {
    setLoading(true);
    getEvents(q ? { q } : undefined)
      .then(setEvents)
      .catch((e) => setErr(e?.response?.data ?? e.message ?? "Failed to load"))
      .finally(() => setLoading(false));
  }, [q]);

  return (
    <div>
      <input
        placeholder="Search events"
        value={q}
        onChange={(e) => setQ(e.target.value)}
      />
      <div style={{ height: 10 }} />
      {loading && <p className="muted">Loading…</p>}
      {!!err && <p style={{ color: "#ef4444" }}>{err}</p>}

      <ul className="grid" style={{ listStyle: "none", padding: 0 }}>
        {events.map((ev) => (
          <li key={ev.id} className="card">
            <h3 className="card-title">
              <Link to={`/events/${ev.id}`}>{ev.title}</Link>
            </h3>
            <div className="muted">
              {eventDate(ev)} {ev.venue ? `• ${ev.venue}` : ""}
            </div>
          </li>
        ))}
      </ul>
    </div>
  );
}
