import { observable, action, makeObservable } from 'mobx';
import axios from 'axios';
import { runInAction } from 'mobx';

class ManufacturerDetails {
    manufacturers = [];

    constructor() {
        makeObservable(this, {
            manufacturers: observable,
            getManufacturers: action,
        });
    }

    async getManufacturers() {
        try {
            const response = await axios.get("https://localhost:7014/api/Manufacturer");
            const manufacturersData = response.data;
            runInAction(() => {
                this.manufacturers = manufacturersData;
            });
            return manufacturersData;
        } catch (error) {
            console.error('Error fetching manufacturers:', error);
            return [];
        }
    }
}

export default new ManufacturerDetails();
