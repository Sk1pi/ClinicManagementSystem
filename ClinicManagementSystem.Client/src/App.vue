<template>
  <div class="container mt-5">
    <h1 class="mb-4 text-center">Система управління клінікою</h1>

    <div class="card mb-4 shadow-sm">
      <div class="card-header bg-primary text-white">
        <h4 class="mb-0">{{ isEditing ? 'Редагувати відділ' : 'Додати новий відділ' }}</h4>
      </div>
      <div class="card-body">
        <form @submit.prevent="isEditing ? updateDepartment() : addDepartment()">
          <div class="row">
            <div class="col-md-5">
              <input v-model="form.name" type="text" class="form-control" placeholder="Назва відділу (напр. Хірургія)" required>
            </div>
            <div class="col-md-5">
              <input v-model="form.location" type="text" class="form-control" placeholder="Розташування (напр. 2 поверх)" required>
            </div>
            <div class="col-md-2">
              <button type="submit" class="btn w-100" :class="isEditing ? 'btn-success' : 'btn-primary'">
                {{ isEditing ? 'Зберегти' : 'Додати' }}
              </button>
            </div>
          </div>
          <button v-if="isEditing" @click="cancelEdit" type="button" class="btn btn-secondary mt-3 btn-sm">Скасувати редагування</button>
        </form>
      </div>
    </div>

    <div class="card shadow-sm">
      <div class="card-body">
        <h4 class="card-title mb-3">Список відділів</h4>
        <table class="table table-hover table-bordered">
          <thead class="table-light">
          <tr>
            <th>Назва</th>
            <th>Розташування</th>
            <th class="text-center">Дії</th>
          </tr>
          </thead>
          <tbody>
          <tr v-for="dept in departments" :key="dept.id">
            <td>{{ dept.name }}</td>
            <td>{{ dept.location }}</td>
            <td class="text-center">
              <button @click="editDepartment(dept)" class="btn btn-sm btn-outline-warning me-2">Редагувати</button>
              <button @click="deleteDepartment(dept.id)" class="btn btn-sm btn-outline-danger">Видалити</button>
            </td>
          </tr>
          <tr v-if="departments.length === 0">
            <td colspan="3" class="text-center text-muted">Немає жодного відділу. Додайте перший!</td>
          </tr>
          </tbody>
        </table>
      </div>
    </div>
  </div>
</template>

<script setup>
import { ref, onMounted } from 'vue';
import axios from 'axios';

const API_URL = 'https://localhost:5267/api/Departments';

const departments = ref([]);
const isEditing = ref(false);
const editingId = ref(null);

const form = ref({
  name: '',
  location: ''
});

const fetchDepartments = async () => {
  try {
    const response = await axios.get(API_URL);
    departments.value = response.data;
  } catch (error) {
    console.error('Помилка при завантаженні:', error);
  }
};

const addDepartment = async () => {
  try {
    await axios.post(API_URL, form.value);
    form.value = { name: '', location: '' };
    await fetchDepartments();
  } catch (error) {
    console.error('Помилка при додаванні:', error);
  }
};

const editDepartment = (dept) => {
  isEditing.value = true;
  editingId.value = dept.id;
  form.value = { name: dept.name, location: dept.location };
};

const cancelEdit = () => {
  isEditing.value = false;
  editingId.value = null;
  form.value = { name: '', location: '' };
};

const updateDepartment = async () => {
  try {
    const payload = { id: editingId.value, ...form.value };
    await axios.put(`${API_URL}/${editingId.value}`, payload);
    cancelEdit();
    await fetchDepartments();
  } catch (error) {
    console.error('Помилка при оновленні:', error);
  }
};

const deleteDepartment = async (id) => {
  if (confirm('Ви впевнені, що хочете видалити цей відділ?')) {
    try {
      await axios.delete(`${API_URL}/${id}`);
      await fetchDepartments();
    } catch (error) {
      console.error('Помилка при видаленні:', error);
    }
  }
};

onMounted(() => {
  fetchDepartments();
});
</script>

<style scoped>
body {
  background-color: #f8f9fa;
}
</style>