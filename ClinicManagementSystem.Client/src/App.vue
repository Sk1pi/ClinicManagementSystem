<template>
  <div class="min-vh-100 bg-light pb-5">
    <nav class="navbar navbar-expand-lg navbar-dark bg-primary shadow-sm mb-4">
      <div class="container">
        <a class="navbar-brand fw-bold" href="#">🏥 Clinic CMS</a>
        <div class="d-flex">
          <button class="btn me-2" :class="currentTab === 'departments' ? 'btn-light' : 'btn-outline-light'" @click="currentTab = 'departments'">
            Відділи
          </button>
          <button class="btn" :class="currentTab === 'patients' ? 'btn-light' : 'btn-outline-light'" @click="currentTab = 'patients'">
            Пацієнти
          </button>
        </div>
      </div>
    </nav>

    <div class="container">
      <div v-if="currentTab === 'departments'">
        <h2 class="mb-4">Управління відділами</h2>

        <div class="card mb-4 shadow-sm border-0">
          <div class="card-body bg-white rounded">
            <h5 class="card-title text-primary mb-3">{{ isDeptEditing ? '✏️ Редагувати відділ' : '➕ Додати відділ' }}</h5>
            <form @submit.prevent="isDeptEditing ? updateDepartment() : addDepartment()">
              <div class="row g-2">
                <div class="col-md-5">
                  <input v-model="deptForm.name" type="text" class="form-control" placeholder="Назва (напр. Хірургія)" required>
                </div>
                <div class="col-md-5">
                  <input v-model="deptForm.location" type="text" class="form-control" placeholder="Розташування" required>
                </div>
                <div class="col-md-2">
                  <button type="submit" class="btn w-100" :class="isDeptEditing ? 'btn-success' : 'btn-primary'">
                    {{ isDeptEditing ? 'Зберегти' : 'Додати' }}
                  </button>
                </div>
              </div>
              <button v-if="isDeptEditing" @click="cancelDeptEdit" type="button" class="btn btn-link text-secondary mt-2 p-0">Скасувати</button>
            </form>
          </div>
        </div>

        <div class="card shadow-sm border-0">
          <div class="card-body">
            <table class="table table-hover align-middle">
              <thead class="table-light">
              <tr>
                <th>Назва</th>
                <th>Розташування</th>
                <th class="text-end">Дії</th>
              </tr>
              </thead>
              <tbody>
              <tr v-for="dept in departments" :key="dept.id">
                <td class="fw-bold">{{ dept.name }}</td>
                <td>{{ dept.location }}</td>
                <td class="text-end">
                  <button @click="editDepartment(dept)" class="btn btn-sm btn-outline-warning me-2">Редаг.</button>
                  <button @click="deleteDepartment(dept.id)" class="btn btn-sm btn-outline-danger">Видалити</button>
                </td>
              </tr>
              <tr v-if="departments.length === 0">
                <td colspan="3" class="text-center text-muted py-4">Немає відділів. Додайте перший!</td>
              </tr>
              </tbody>
            </table>
          </div>
        </div>
      </div>

      <div v-if="currentTab === 'patients'">
        <h2 class="mb-4">Управління пацієнтами</h2>

        <div class="card mb-4 shadow-sm border-0">
          <div class="card-body bg-white rounded">
            <h5 class="card-title text-primary mb-3">{{ isPatEditing ? '✏️ Редагувати пацієнта' : '➕ Додати пацієнта' }}</h5>
            <form @submit.prevent="isPatEditing ? updatePatient() : addPatient()">
              <div class="row g-2 mb-2">
                <div class="col-md-4">
                  <input v-model="patForm.firstName" type="text" class="form-control" placeholder="Ім'я" required>
                </div>
                <div class="col-md-4">
                  <input v-model="patForm.lastName" type="text" class="form-control" placeholder="Прізвище" required>
                </div>
                <div class="col-md-4">
                  <input v-model="patForm.dateOfBirth" type="date" class="form-control" :max="todayDate" required>
                </div>
              </div>
              <div class="row g-2">
                <div class="col-md-5">
                  <input v-model="patForm.phoneNumber" type="text" class="form-control" placeholder="Телефон (тільки цифри)" @input="validatePhoneNumber" required>
                </div>
                <div class="col-md-5">
                  <input v-model="patForm.email" type="email" class="form-control" placeholder="Email" required>
                </div>
                <div class="col-md-2">
                  <button type="submit" class="btn w-100" :class="isPatEditing ? 'btn-success' : 'btn-primary'">
                    {{ isPatEditing ? 'Зберегти' : 'Додати' }}
                  </button>
                </div>
              </div>
              <button v-if="isPatEditing" @click="cancelPatEdit" type="button" class="btn btn-link text-secondary mt-2 p-0">Скасувати</button>
            </form>
          </div>
        </div>

        <div class="card shadow-sm border-0">
          <div class="card-body">
            <table class="table table-hover align-middle">
              <thead class="table-light">
              <tr>
                <th>ПІБ</th>
                <th>Дата народження</th>
                <th>Контакти</th>
                <th class="text-end">Дії</th>
              </tr>
              </thead>
              <tbody>
              <tr v-for="pat in patients" :key="pat.id">
                <td class="fw-bold">{{ pat.firstName }} {{ pat.lastName }}</td>
                <td>{{ new Date(pat.dateOfBirth).toLocaleDateString() }}</td>
                <td>
                  <div>📞 {{ pat.phoneNumber }}</div>
                  <div class="text-muted small">✉️ {{ pat.email }}</div>
                </td>
                <td class="text-end">
                  <button @click="editPatient(pat)" class="btn btn-sm btn-outline-warning me-2">Редаг.</button>
                  <button @click="deletePatient(pat.id)" class="btn btn-sm btn-outline-danger">Видалити</button>
                </td>
              </tr>
              <tr v-if="patients.length === 0">
                <td colspan="4" class="text-center text-muted py-4">Немає пацієнтів. Додайте першого!</td>
              </tr>
              </tbody>
            </table>
          </div>
        </div>
      </div>

    </div>
  </div>
</template>

<script setup>
import { ref, onMounted } from 'vue';
import axios from 'axios';

const API_BASE = 'http://localhost:5267/api';

const currentTab = ref('departments');

const todayDate = new Date().toISOString().split('T')[0];

const departments = ref([]);
const isDeptEditing = ref(false);
const editingDeptId = ref(null);
const deptForm = ref({ name: '', location: '' });

const fetchDepartments = async () => {
  try {
    const response = await axios.get(`${API_BASE}/Departments`);
    departments.value = response.data;
  } catch (error) {
    console.error('Помилка завантаження відділів:', error);
  }
};

const addDepartment = async () => {
  try {
    await axios.post(`${API_BASE}/Departments`, deptForm.value);
    deptForm.value = { name: '', location: '' };
    await fetchDepartments();
  } catch (error) {
    console.error('Помилка додавання відділу:', error);
  }
};

const editDepartment = (dept) => {
  isDeptEditing.value = true;
  editingDeptId.value = dept.id;
  deptForm.value = { name: dept.name, location: dept.location };
};

const cancelDeptEdit = () => {
  isDeptEditing.value = false;
  editingDeptId.value = null;
  deptForm.value = { name: '', location: '' };
};

const updateDepartment = async () => {
  try {
    const payload = { id: editingDeptId.value, ...deptForm.value };
    await axios.put(`${API_BASE}/Departments/${editingDeptId.value}`, payload);
    cancelDeptEdit();
    await fetchDepartments();
  } catch (error) {
    console.error('Помилка оновлення відділу:', error);
  }
};

const deleteDepartment = async (id) => {
  if (confirm('Ви впевнені, що хочете видалити цей відділ?')) {
    try {
      await axios.delete(`${API_BASE}/Departments/${id}`);
      await fetchDepartments();
    } catch (error) {
      console.error('Помилка видалення відділу:', error);
    }
  }
};

const patients = ref([]);
const isPatEditing = ref(false);
const editingPatId = ref(null);
const patForm = ref({ firstName: '', lastName: '', dateOfBirth: '', phoneNumber: '', email: '' });

const validatePhoneNumber = () => {
  patForm.value.phoneNumber = patForm.value.phoneNumber.replace(/\D/g, '');
};

const fetchPatients = async () => {
  try {
    const response = await axios.get(`${API_BASE}/Patients`);
    patients.value = response.data;
  } catch (error) {
    console.error('Помилка завантаження пацієнтів:', error);
  }
};

const addPatient = async () => {
  try {
    await axios.post(`${API_BASE}/Patients`, patForm.value);
    patForm.value = { firstName: '', lastName: '', dateOfBirth: '', phoneNumber: '', email: '' };
    await fetchPatients();
  } catch (error) {
    console.error('Помилка додавання пацієнта:', error);
  }
};

const editPatient = (pat) => {
  isPatEditing.value = true;
  editingPatId.value = pat.id;
  const dateStr = pat.dateOfBirth ? pat.dateOfBirth.split('T')[0] : '';
  patForm.value = {
    firstName: pat.firstName,
    lastName: pat.lastName,
    dateOfBirth: dateStr,
    phoneNumber: pat.phoneNumber,
    email: pat.email
  };
};

const cancelPatEdit = () => {
  isPatEditing.value = false;
  editingPatId.value = null;
  patForm.value = { firstName: '', lastName: '', dateOfBirth: '', phoneNumber: '', email: '' };
};

const updatePatient = async () => {
  try {
    const payload = { id: editingPatId.value, ...patForm.value };
    await axios.put(`${API_BASE}/Patients/${editingPatId.value}`, payload);
    cancelPatEdit();
    await fetchPatients();
  } catch (error) {
    console.error('Помилка оновлення пацієнта:', error);
  }
};

const deletePatient = async (id) => {
  if (confirm('Ви впевнені, що хочете видалити цього пацієнта?')) {
    try {
      await axios.delete(`${API_BASE}/Patients/${id}`);
      await fetchPatients();
    } catch (error) {
      console.error('Помилка видалення пацієнта:', error);
    }
  }
};

onMounted(() => {
  fetchDepartments();
  fetchPatients();
});
</script>

<style>
body {
  font-family: 'Segoe UI', Tahoma, Geneva, Verdana, sans-serif;
}
.card {
  transition: transform 0.2s ease-in-out;
}
.card:hover {
  transform: translateY(-2px);
}
</style>