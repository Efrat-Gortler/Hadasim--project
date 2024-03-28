import { observable, action, makeObservable } from 'mobx';
import axios from 'axios';
import { runInAction } from 'mobx';

class MemberDetails {
  members = [

  ];

  constructor() {
    // Ensure that makeObservable is imported and used properly
    makeObservable(this, {
      members: observable,
      postMember: action,
      getMember: action,
      fetchMembers: action,
    });
  }

  // async getMember() {
  //   try {
  //     const response = await axios.get("https://localhost:7014/api/Members");
  //     const membersData = response.data; // Extract member data from response
  //     runInAction(() => {
  //       console.log(membersData);
  //       console.log(data.map(member => member.Id))
  //       this.members = membersData;
  //     });
  //     return membersData; // Return member data
  //   } catch (error) {
  //     console.error('Error fetching members:', error);
  //     // Optionally, set this.members to an empty array or handle the error in another way
  //     return []; // Return an empty array in case of error
  //   }
  // }
  async getMember() {
    try {
      const response = await axios.get("https://localhost:7014/api/Members");
      const membersData = response.data; // Extract member data from response
      runInAction(() => {
        console.log(membersData);
        console.log(membersData.map(member => member.Id)); // Use membersData instead of data
        this.members = membersData;
      });
      return membersData; // Return member data
    } catch (error) {
      console.error('Error fetching members:', error);
      // Optionally, set this.members to an empty array or handle the error in another way
      return []; // Return an empty array in case of error
    }
  
  }
  deleteMember = async (id) => {
    try {
      await axios.delete(`https://localhost:7014/api/Members/${id}`);
      runInAction(() => {
        // Remove the member from the local list after deletion
        this.members = this.members.filter(member => member.id !== id);
      });
    } catch (error) {
      console.error('Error deleting member:', error);
    }
  };

  // postMember(m) {
  //   fetch("https://localhost:7014/api/Members", {
  //     method: 'POST',
  //     headers: { 'Content-Type': 'application/json' },
  //     body: JSON.stringify(m)
  //   });
  // }
 async postMember(member) {
    try {
        // Trim unnecessary whitespaces from member properties
        member.identity = member.identity.trim();
        member.name = member.name.trim();
        member.cityId = parseInt(member.cityId.trim());
        member.mobilePhone = member.mobilePhone.trim();

        console.log(member);
        const response = await axios.post("https://localhost:7014/api/Members", member);
        const newMember = response.data;
        runInAction(() => {
            this.members.push(newMember);
        });
        return newMember;
    } catch (error) {
        console.error('Error posting member:', error);
        throw error; // Re-throw the error for handling at a higher level
    }
}
  async putMember(member) {
    try {
      const response = await axios.put(`https://localhost:7014/api/Members/${member.id}`, member);
      const updatedMember = response.data;
      runInAction(() => {
        // Replace the existing member with the updated one in the members array
        const index = this.members.findIndex(m => m.id === updatedMember.id);
        if (index !== -1) {
          this.members[index] = updatedMember;
        }
      });
      return updatedMember;
    } catch (error) {
      console.error('Error updating member:', error);
      throw error; // Re-throw the error for handling at a higher level
    }
  }
  
//   async addVaccination(s) {
//     try {
//         const response = await axios.post("https://localhost:7014/api/Vaccination", s);
//         runInAction(() => {
//             this.vaccinations.push(response.data);
//             memberDetails.addVaccinationForMember(vaccination.memberId);
//         });
//     } catch (error) {
//         console.error('Error adding vaccination:', error);
//     }
// }
  

  fetchMembers(members) {
    this.members = members;
  }
}

export default new MemberDetails();




// import { observable, action, makeObservable } from 'mobx';
// import axios from 'axios';
// import {runInAction} from 'mobx';

// class MemberDetails {
//   members = [];

//   // constructor() {
//   //   makeObservable(this, {
//   //     members: observable,
//   //     postMember: action,
//   //     getMember: action // Remove 'computed' decorator
//   //   });
//   // }
//   constructor() {
//     makeObservable(this, {
//       members: observable,
//       postMember: action,
//       getMember:  action,
//       fetchMembers: action ,
//     });
//   }


// //   getMember = async () => {
// //     try {
// //         const response = await axios.get("https://localhost:7014/api/Members");
// //         runInAction(() => {
// //             console.log(response.data);
// //             this.members = response.data;
// //         });
// //     } catch (error) {
// //         console.error('Error fetching members:', error);
// //     }
// // }
// getMember = async () => {
//   try {
//      const response = await axios.get("https://localhost:7014/api/Members");
//      runInAction(() => {
//         console.log(response.data);
//         this.members = response.data;
//      });
//   } catch (error) {
//      console.error('Error fetching members:', error);
//      // Optionally, set this.members to an empty array or handle the error in another way
//   }
// }


//   postMember(m) {
//     fetch("https://localhost:7014/api/Members", {
//       method: 'POST',
//       headers: { 'Content-Type': 'application/json' },
//       body: JSON.stringify(m)
//     });
//   }
//      // Action to update members
//      fetchMembers(members) {
//       this.members = members;
//   }
  
// }

// export default new MemberDetails();
