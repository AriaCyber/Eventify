import { useEffect, useState } from "react";
import { useParams, Link, useNavigate } from "react-router-dom";
import { getEvent } from "../api/events";
import type { EventDetail } from "../types";
import { eventDate, eventTiers } from "../utils";

export default function EventDetails() {
  const { id } = useParams();
  const nav = useNavigate();
  const [data, setData] = useState<EventDetail | null>(null);
  const [loading, setLoading] = useState(true);
  const [err, setErr] = useState("");

  useEffect(() => {
    setLoading(true);
    getEvent(Number(id))
      .then(setData)
      .catch((e) => setErr(e?.response?.data ?? e.message ?? "Failed"))
      .finally(() => setLoading(false));
  }, [id]);

  if (loading) return <p className="muted">Loading…</p>;
  if (err) return <p style={{ color: "#ef4444" }}>{err}</p>;
  if (!data) return <p>Not found.</p>;

  const tiers = eventTiers(data) as any[];

  return (
    <div>
      <h1 style={{ margin: "0 0 6px" }}>{data.title}</h1>
      <div className="muted">
        {eventDate(data)} {data.venue ? `• ${data.venue}` : ""}
      </div>
      {data.description && <p style={{ marginTop: 12 }}>{data.description}</p>}

      <div className="section">Tickets</div>
      {tiers.length === 0 && <p className="muted">No tiers defined.</p>}
      <ul style={{ listStyle: "none", padding: 0 }}>
        {tiers.map((t) => (
          <li
            key={t.id}
            className="card"
            style={{
              display: "flex",
              alignItems: "center",
              justifyContent: "space-between",
            }}
          >
            <div>
              <div className="card-title" style={{ margin: 0 }}>
                {t.name}
              </div>
              <div className="muted">
                ${typeof t.price === "number" ? t.price.toFixed(2) : t.price} •{" "}
                {(t.capacity ?? 0) - (t.sold ?? 0)} left
              </div>
            </div>

            <Link to={`/events/${data.id}/checkout`} state={{ tierId: t.id }}>
              <button>Book</button>
            </Link>
          </li>
        ))}
      </ul>

      <div className="section">
        <Link to={`/events/${data.id}/reviews`}>View reviews</Link>
      </div>
    </div>
  );
}
