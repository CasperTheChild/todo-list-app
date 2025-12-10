import { useEffect, useState } from 'react'
import PageButton from './PageButton.jsx'
import TodoListTable from '../todos/TodoListTable.jsx'
import CreateTodoListForm from '../todos/CreateTodoListForm.jsx'
import apiService from '../api/TodoListApi.js'
import { useAuth } from '../components/AuthContext.jsx'
import { LogOutButton } from './LogOutButton.jsx'
import { Register } from '../auth/Register.jsx'

function MainPage() {
    const { token } = useAuth();

    const [todoList, setTodoList] = useState({
        items: [],
        totalCount: 0,
        pageNum: 1,
        pageSize: 4,
        totalPages: 0,
    })

    const [pageSize, setPageSize] = useState(4)
    const [pageNum, setPageNum] = useState(1)

    useEffect(() => {
        apiService.getPagedTodoList(pageNum, pageSize, token)
            .then(data => setTodoList(data));
    }, [pageNum, pageSize])

    async function handleCreateTodoList(newTodoList) {
        try {
            await apiService.createTodoList(newTodoList, token);
            const updated = await apiService.getPagedTodoList(pageNum, pageSize, token);
            setTodoList(updated);
        }
        catch (error) {
            console.error("Failed to create todo list:", error);
        }
    }

    async function handleDeleteTodoList(id) {
        try {
            await apiService.deleteTodoList(id, token);
            const updated = await apiService.getPagedTodoList(pageNum, pageSize, token);
            setTodoList(updated);
        }
        catch (error) {
            console.error("Failed to create todo list:", error);
        }
    }

    async function handleEditTodoList(id, newTodoList) {
        try {
            await apiService.editTodoList(id, newTodoList, token);
            const updated = await apiService.getPagedTodoList(pageNum, pageSize, token);
            setTodoList(updated);
        }
        catch (error) {
            console.error("Failed to create todo list:", error);
        }
    }

    return (
        <>
            <TodoListTable
                todoList={todoList}
                pageNum={pageNum}
                setPageNum={setPageNum}
                handleDelete={handleDeleteTodoList}
                handleEdit={handleEditTodoList}
            />

            <CreateTodoListForm
                onCreate={handleCreateTodoList}
            />

            <LogOutButton></LogOutButton>
        </>
    )
}

export default MainPage