import { observable, action, makeObservable } from 'mobx';
import axios from 'axios';
import { runInAction } from 'mobx';

class VaccinationDetails {
    vaccinations = [];

    constructor() {
        makeObservable(this, {
            vaccinations: observable,
            getVaccinations: action,
            addVaccination: action,
            updateVaccination: action,
        });
    }

    async getVaccinations() {
        try {
            const response = await axios.get("https://localhost:7014/api/Vaccination");
            const vaccinationsData = response.data;
            runInAction(() => {
                this.vaccinations = vaccinationsData;
            });
            return vaccinationsData;
        } catch (error) {
            console.error('Error fetching vaccinations:', error);
            return [];
        }
    }

    async addVaccination(newVaccination) {
        try {
            const response = await axios.post("https://localhost:7014/api/Vaccination", newVaccination);
            runInAction(() => {
                this.vaccinations.push(response.data);
            });
            return response.data; // Return the newly added vaccination data
        } catch (error) {
            console.error('Error adding vaccination:', error);
            throw error; // Rethrow the error to handle it in the caller
        }
    }

    async updateVaccination(id, updatedVaccination) {
        try {
            const response = await axios.put(`https://localhost:7014/api/Vaccination/${id}`, updatedVaccination);
            runInAction(() => {
                // Update the corresponding vaccination in the local state
                const index = this.vaccinations.findIndex(v => v.id === id);
                if (index !== -1) {
                    this.vaccinations[index] = response.data;
                }
            });
            return response.data; // Return the updated vaccination data
        } catch (error) {
            console.error('Error updating vaccination:', error);
            throw error; // Rethrow the error to handle it in the caller
        }
    }
}

export default new VaccinationDetails();

