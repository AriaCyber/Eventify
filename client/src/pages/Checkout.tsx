import { useLocation, useParams } from "react-router-dom";
import { useState } from "react";
import { checkout } from "../api/bookings";

export default function Checkout(){
  const { id } = useParams();
  const { state } = useLocation() as { state?: { tierId?: number } };
  const [email, setEmail] = useState("");
  const [tierId, setTierId] = useState<number>(state?.tierId ?? 0);
  const [qty, setQty] = useState(1);
  const [promo, setPromo] = useState("");
  const [busy, setBusy] = useState(false);
  const [res, setRes] = useState<{orderId:number; ticketSerials:string[]}>();

  async function submit(){
    if(!email || !tierId || qty < 1) return alert("Fill all fields");
    setBusy(true);
    try{
      const r = await checkout({ eventId:Number(id), tierId, quantity:qty, email, promoCode: promo || undefined });
      setRes(r);
    }catch(e:any){ alert(e?.response?.data || e.message || "Failed"); }
    finally{ setBusy(false); }
  }

  if(res){
    return (<div><h3>Booking Confirmed</h3><p>Order #{res.orderId}</p><ul>{res.ticketSerials.map(s => <li key={s}>{s}</li>)}</ul></div>);
  }

  return (
    <div>
      <h3>Checkout</h3>
      <label>Email<br/><input value={email} onChange={e=>setEmail(e.target.value)} placeholder="you@example.com"/></label><br/><br/>
      <label>Tier Id<br/><input type="number" value={tierId} onChange={e=>setTierId(Number(e.target.value))}/></label><br/><br/>
      <label>Quantity<br/><input type="number" min={1} value={qty} onChange={e=>setQty(Number(e.target.value))}/></label><br/><br/>
      <label>Promo Code<br/><input value={promo} onChange={e=>setPromo(e.target.value)}/></label><br/><br/>
      <button disabled={busy} onClick={submit}>Place Order</button>
    </div>
  );
}
