import { useEffect, useState } from "react";
import { useParams } from "react-router-dom";
import { listReviews, addReview } from "../api/reviews";

export default function Reviews(){
  const { id } = useParams();
  const eventId = Number(id);
  const [items, setItems] = useState<any[]>([]);
  const [rating, setRating] = useState(5);
  const [comment, setComment] = useState("");
  const [email, setEmail] = useState("");

  async function load(){ setItems(await listReviews(eventId)); }
  useEffect(()=>{ load(); }, [eventId]);

  async function submit(){
    if(!email || !comment) return alert("Fill all fields");
    await addReview(eventId, rating, comment, email);
    setComment(""); setRating(5);
    await load();
  }

  return (
    <div>
      <h3>Reviews</h3>
      <ul>{items.map(r => <li key={r.id}>{r.rating}/5 — {r.comment}</li>)}</ul>
      <h4>Add a review</h4>
      <input placeholder="Email" value={email} onChange={e=>setEmail(e.target.value)} /><br/><br/>
      <input type="number" min={1} max={5} value={rating} onChange={e=>setRating(Number(e.target.value))} /><br/><br/>
      <textarea placeholder="Comment" value={comment} onChange={e=>setComment(e.target.value)} /><br/><br/>
      <button onClick={submit}>Submit</button>
    </div>
  );
}
