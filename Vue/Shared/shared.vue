<template>
    <div class="modal hidden" tabindex="-1" id="confirm-modal">
        <div class="modal-dialog modal-sm">
            <div class="modal-content">
                <div class="modal-header">
                    <h4 class="modal-title">Kas Te olete kindel?</h4>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-main" @click="$emit('confirmed', {})">Jah</button>
                    <button type="button" class="btn btn-secondary" @click="hide">Ei</button>
                </div>
            </div>
        </div>
    </div>
</template>

<script>
    export default {
        name: 'confirm',
        data() {
            return {

            }
        },
        methods: {
            hide: function () {
                $("#confirm-modal").removeClass("shown");
            }
        }
    }
</script>