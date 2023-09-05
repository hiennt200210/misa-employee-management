<template>
    <!-- Paging -->
    <div class="paging">
        <div class="m-paging">
            <!-- Paging Left -->
            <div class="m-paging-left">
                Tổng: <span class="number">4</span> bản ghi
            </div>

            <!-- Paging Right -->
            <div class="m-paging-right">
                <div class="record-per-page">
                    Số bản ghi/trang:
                    <div class="m-dropdownlist">
                        <select name="">
                            <option value="10">10</option>
                            <option value="20">20</option>
                            <option value="50">50</option>
                            <option value="100">100</option>
                        </select>
                    </div>
                </div>

                <div>1 - 4 bản ghi</div>

                <div class="m-paging-button">
                    <MButton type="title-bar" icon="prev" />
                    <MButton type="title-bar" icon="next" />
                </div>
            </div>
        </div>
    </div>
</template>

<script>
import MButton from "../button/MButton.vue";

export default {
    name: "MPaging",
    components: {
        MButton,
    },
};
</script>

<style scoped>
.m-paging {
    height: 48px;
    border-bottom-left-radius: 3px;
    border-bottom-right-radius: 3px;
    padding: 0px 12px;
    display: flex;
    align-items: center;
    justify-content: space-between;
    background-color: #f5f5f5;
}

.m-paging-left .number {
    font-family: "Google Sans Bold", sans-serif;
}

.m-paging-right {
    display: flex;
    align-items: center;
}

.m-paging-right > * {
    margin-left: 16px;
}

.m-paging-right .record-per-page {
    display: flex;
    align-items: center;
}

.m-paging-right .record-per-page select {
    color: var(--color-label);
    padding: 0 8px;
    margin-left: 8px;
}

.m-paging-button {
    display: flex;
    align-items: center;
}

.m-paging-button > *:not(:last-child) {
    margin-right: 8px;
}
</style>
