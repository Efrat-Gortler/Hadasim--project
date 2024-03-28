import { observable, action, makeObservable } from 'mobx';
import axios from 'axios';
import { runInAction } from 'mobx';

class CityDetails {
    cities = [];

    constructor() {
        makeObservable(this, {
            cities: observable,
            getCities: action,
        });
    }

    async getCities() {
        try {
            const response = await axios.get("https://localhost:7014/api/City");
            const citiesData = response.data;
            runInAction(() => {
                this.cities = citiesData;
            });
            return citiesData;
        } catch (error) {
            console.error('Error fetching cities:', error);
            return [];
        }
    }
}

export default new CityDetails();
