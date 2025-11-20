async function getPagedTodoList(pageNum, pageSize) {
    const url = `/api/TodoList/paged?pageNum=${pageNum}&pageSize=${pageSize}`;
    const response = await fetch(url);
    const data = await response.json();
    return data;
}

async function createTodoList(todoList) {
    const url = `/api/TodoList`
    const res = await fetch(url, {
        method: 'POST',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify(todoList),
    });

    if (!res.ok) {
        throw new Error('Failed to create todo list');
    }

    return res.json();
}

async function deleteTodoList(id) {
    const url = `/api/TodoList/${id}`
    const response = await fetch(url, {
        method: 'DELETE'
    });

    if (response.status === 204) {
        return true;
    }

    throw new Error("Failed to delete todo list");
}

async function editTodoList(id, newTodoList) {
    const url = `/api/TodoList/${id}`
    const res = await fetch(url, {
        method: 'PUT',
        body: JSON.stringify(newTodoList),
        headers: { "Content-Type": "application/json" }
    })

    if (!res.ok) {
        throw new Error('Failed to create todo list');
    }

    return true;
}

export default {
    getPagedTodoList,
    createTodoList,
    deleteTodoList,
    editTodoList,
};