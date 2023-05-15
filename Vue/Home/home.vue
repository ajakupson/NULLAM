<script>
    import confirm from './../Shared/shared.vue';

    export default {
        components: {
            confirm
        },
        data() {
            return {
                events: eventsList,
                selected: null
            }
        },
        mounted() {
            
        },
        methods: {
            confrimRemove: function (evt) {
                this.selected = evt;
                $("#confirm-modal").addClass("shown");
            },
            removeEvent() {
                let that = this;
                $.ajax({
                    url: `/event/${this.selected.Id}/remove`,
                    type: 'POST',
                    contentType: "application/json",
                    data: JSON.stringify(this.newEvent),
                    success: function (response) {
                        that.events = that.events.filter(e => e.Id != that.selected.Id);

                        $("#confirm-modal").removeClass("shown");
                        toastr.success("Üritus on edukalt kustutatud", "Edu");
                    },
                    error: function (xhr, textStatus, errorThrown) {
                        console.log(errorThrown);
                    }
                });
            }
        },
        computed: {
            actives() {
                return this.events.filter(e => e.IsActive);
            },
            inactives() {
                return this.events.filter(e => !e.IsActive);
            }
        }
    }
</script>

<template>
    <div class="events">
        <div class="active-events">
            <h1>Tulevased üritused</h1>
            <ul>
                <li v-for="event in actives">
                    <span>{{ event.Name }}</span>
                    <span>{{ event.DateTimeFormatted }}</span>
                    <span class="d-flex flex-jcc flex-aic"><a :href="'/event/' + event.Id + '/participants'" class="btn mr10">Osavõtjad</a><i class="fa fa-times" aria-hidden="true" @click="confrimRemove(event)"></i></span>
                </li>
            </ul>
            <a href="/Event" class="btn">Lisa üritus</a>
        </div>
        <div class="inactive-events">
            <h1>Toimunud üritused</h1>
            <ul>
                <li v-for="event in inactives">
                    <span>{{ event.Name }}</span>
                    <span>{{ event.DateTimeFormatted }}</span>
                    <span class="d-flex flex-jcc flex-aic"><a :href="'/event/' + event.Id + '/participants'" class="btn mr10">Osavõtjad</a></span>
                </li>
            </ul>
        </div>
    </div>

    <confirm @confirmed="removeEvent"></confirm>
</template>