import { useEffect, useState } from "react";
import { getRefunds, updateRefundStatus } from "./adminApi";

export default function Refunds(){
  const [items, setItems] = useState<any[]>([]);

  async function load(){
    setItems(await getRefunds());
  }

  useEffect(() => { load(); }, []);

  async function approve(id:number){
    await updateRefundStatus(id, "Approved");
    load();
  }

  return (
    <div>
      <h2>Refund Requests</h2>

      <ul>
        {items.map(r => (
          <li key={r.refundId}>
            Order #{r.orderId} — {r.status}
            <button onClick={() => approve(r.refundId)}>Approve</button>
          </li>
        ))}
      </ul>
    </div>
  );
}
