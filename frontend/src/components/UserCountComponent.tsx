import React, { useEffect, useState } from 'react';
import * as signalR from '@microsoft/signalr';

const UserCountComponent = () => {
    const [onlineUsers, setOnlineUsers] = useState<number>(0);

    useEffect(() => {
        const connection = new signalR.HubConnectionBuilder()
            .withUrl('http://localhost:5169/userCountHub', {
                skipNegotiation: true,
                transport: signalR.HttpTransportType.WebSockets
            })
            .withAutomaticReconnect()
            .configureLogging(signalR.LogLevel.Information)
            .build();

        const connectSignalR = async () => {
            try {
                await connection.start();
                console.log('Connected to SignalR hub');
                connection.on('UpdateUserCount', (count: number) => {
                    setOnlineUsers(count);
                });
            } catch (error) {
                console.error('Error starting connection: ', error);
            }
        };

        connectSignalR();

        return () => {
            connection.stop().then(() => console.log('Disconnected from SignalR hub')).catch((err) => console.error("Error stopping connection: ", err));
        };
    }, []);

    return (
        <span>Online Users: {onlineUsers}</span>
    );
};

export default UserCountComponent;
