import { useEffect, useState } from 'react'
import PageButton from './components/PageButton.jsx'
import TodoListTable from './components/TodoListTable.jsx'
import CreateTodoListForm from './components/CreateTodoListForm.jsx'
import apiService from './api/TodoListApi.js'

function App() {
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
        apiService.getPagedTodoList(pageNum, pageSize)
            .then(data => setTodoList(data));
    }, [pageNum, pageSize])

    async function handleCreateTodoList(newTodoList) {
        try {
            await apiService.createTodoList(newTodoList);
            const updated = await apiService.getPagedTodoList(pageNum, pageSize);
            setTodoList(updated);
        }
        catch (error) {
            console.error("Failed to create todo list:", error);
        }
    }

    async function handleDeleteTodoList(id) {
        try {
            await apiService.deleteTodoList(id);
            const updated = await apiService.getPagedTodoList(pageNum, pageSize);
            setTodoList(updated);
        }
        catch (error) {
            console.error("Failed to create todo list:", error);
        }
    }

    async function handleEditTodoList(id, newTodoList) {
        try {
            await apiService.editTodoList(id, newTodoList);
            const updated = await apiService.getPagedTodoList(pageNum, pageSize);
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
        </>
    )
}

export default App
