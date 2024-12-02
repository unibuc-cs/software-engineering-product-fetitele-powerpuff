import React, { useEffect, useState } from 'react';
import axios from 'axios';
import { RetetaDisplay } from '../components/retete/RetetaDisplay';
export default function ReteteSummary(){
    const [reteteArray, setReteteArray] = useState([]);

    useEffect(() => {
        async function get_recipes() {
            try {
                const token = localStorage.getItem('token');
                const response = await axios.get('https://localhost:7094/api/Recipe', {
                    headers: {
                        Authorization: `Bearer ${token}`,
                    },
                });

                setReteteArray(response.data);
             
            } catch (error) {
                console.log(error);
            }
        }

        get_recipes();
    },[]); 

    //to do pentru mai tarziu : daca tokenul expira, nu mai face query
    return (
        <div>
            {reteteArray.map(reteta=><div key={crypto.randomUUID()}><RetetaDisplay reteta={reteta}></RetetaDisplay></div>)}
        </div>
    );
    

}

