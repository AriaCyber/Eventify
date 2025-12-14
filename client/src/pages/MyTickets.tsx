import { useState } from "react";
import { myTickets, requestRefund } from "../api/bookings";

export default function MyTickets(){
  const [email, setEmail] = useState("");
  const [orders, setOrders] = useState<any[]>([]);
  const [loading, setLoading] = useState(false);

  async function load(){
    setLoading(true);
    try{ setOrders(await myTickets(email)); }catch(e:any){ alert(e.message || "Failed"); }
    finally{ setLoading(false); }
  }

  async function refund(orderId:number){
    const reason = prompt("Reason for refund?");
    if(!reason) return;
    await requestRefund(orderId, reason);
    alert("Refund requested");
  }

  return (
    <div>
      <h3>My Tickets</h3>
      <input placeholder="Email used to book" value={email} onChange={e=>setEmail(e.target.value)} />
      <button onClick={load} disabled={loading || !email}>Load</button>
      <ul>
        {orders.map(o => (
          <li key={o.orderId} style={{margin:"8px 0"}}>
            Order #{o.orderId} — {o.status}
            <ul>{o.tickets?.map((t:any)=> <li key={t.serial}>{t.serial}</li>)}</ul>
            <button onClick={()=>refund(o.orderId)}>Request Refund</button>
          </li>
        ))}
      </ul>
    </div>
  );
}
